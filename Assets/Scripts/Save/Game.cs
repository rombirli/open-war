using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Shop;
using Save;
using Shop;
using Unity.VisualScripting;
using UnityEngine;

public class Game
{
    private const string KeyGames = "GamesNames";

    public string Name { get; }
    public string Path => $"GAME:{Name}";

    private Game(string name)
    {
        Name = name;
    }

    public static bool NewGame(string name, out Game game)
    {
        var games = LoadGames();
        game = null;
        name = ValidateGameName(name);
        if (games.Select(game => game.Name).Contains(name))
            return false;
        game = new Game(name);
        Bag.Quantities[Consumable.MainAmmo] = Bag.Capacities[Consumable.MainAmmo];
        Bag.Quantities[Consumable.SecondaryAmmo] = Bag.Capacities[Consumable.SecondaryAmmo];
        Inventory.Money = 0;
        Inventory.Gems = 0;
        Tank.CurrentTank = Tank.FirstTank;
        Turret.CurrentTurret = Turret.FirstTurret;
        Inventory.Health = Inventory.MaxHealth;
        SaveGames(games.Concat(new[] { game }));
        return true;
    }

    public static IEnumerable<Game> LoadGames()
    {
        var s = PlayerPrefs.GetString(KeyGames, "");
        if (s.Length == 0) return new Game[] { };
        return s.Split(',').Select(x => new Game(x));
    }

    private static void SaveGames(IEnumerable<Game> games) =>
        PlayerPrefs.SetString(KeyGames, string.Join(",", games.Select(g => g.Name)));

    public void Delete()
    {
        var games = LoadGames().ToList();
        games.RemoveAll(x => x.Name.Equals(Name));
        PlayerPrefs.SetString(KeyGames, string.Join(",", games.Select(g => g.Name)));
        ChunkSaver.RemoveGameChunks(this);
    }


    public static string ValidateGameName(string name) =>
        StringUtility.Filter(name, true, true, true, false, false);
}