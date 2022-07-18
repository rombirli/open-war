using System.Collections.Generic;
using Save;
using UnityEngine;
using static Gameplay.Shop.Consumable;

namespace Gameplay.Shop
{
    public static class Bag
    {
        private static string PathMainAmmo(string path) => $"{path}/MainAmmo";
        private static string PathSecondaryAmmo(string path) => $"{path}/SecondaryAmmo";
        private static string PathHealthPotions(string path) => $"{path}/HealthPotions";

        public static readonly Dictionary<Consumable, int> Capacities = new()
        {
            { MainAmmo, 200 },
            { SecondaryAmmo, 30 },
            { HealthPotion, 10 }
        };

        public static readonly Dictionary<Consumable, int> Quantities = new()
        {
            { MainAmmo, 100 },
            { SecondaryAmmo, 20 },
            { HealthPotion, 10 }
        };

        public static void Save(string path)
        {
            PlayerPrefs.SetString(PathMainAmmo(path), $"{Quantities[MainAmmo]};{Capacities[MainAmmo]}");
            PlayerPrefs.SetString(PathSecondaryAmmo(path), $"{Quantities[SecondaryAmmo]};{Capacities[SecondaryAmmo]}");
            PlayerPrefs.SetString(PathHealthPotions(path), $"{Quantities[HealthPotion]};{Capacities[HealthPotion]}");
        }

        public static bool Load(string path)
        {
            if (!PlayerPrefs.HasKey(PathMainAmmo(path)) ||
                !PlayerPrefs.HasKey(PathSecondaryAmmo(path)) ||
                !PlayerPrefs.HasKey(PathHealthPotions(path)))
                return false;
            var mainAmmo = PlayerPrefs.GetString(PathMainAmmo(path), "0;0").Split(';');
            var secondaryAmmo = PlayerPrefs.GetString(PathSecondaryAmmo(path), "0;0").Split(';');
            var healthPotions = PlayerPrefs.GetString(PathHealthPotions(path), "0;0").Split(';');
            Quantities[MainAmmo] = int.Parse(mainAmmo[0]);
            Capacities[MainAmmo] = int.Parse(mainAmmo[1]);
            Quantities[SecondaryAmmo] = int.Parse(secondaryAmmo[0]);
            Capacities[SecondaryAmmo] = int.Parse(secondaryAmmo[1]);
            Quantities[HealthPotion] = int.Parse(healthPotions[0]);
            Capacities[HealthPotion] = int.Parse(healthPotions[1]);
            return true;
        }
    }
}