using System;
using System.Collections.Generic;
using System.Linq;
using Save;
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
        SaveGames(games.Concat(new[] { game }));
        Inventory.ForceSet(Inventory.Item.Health, Inventory.GetCapacity(Inventory.Item.Health));
        Inventory.ForceSet(Inventory.Item.MainAmmo, Inventory.GetCapacity(Inventory.Item.MainAmmo));
        Inventory.ForceSet(Inventory.Item.TurretAmmo, Inventory.GetCapacity(Inventory.Item.TurretAmmo));
        Inventory.ForceSet(Inventory.Item.Coin, 0);
        Inventory.ForceSet(Inventory.Item.Key, 0);
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