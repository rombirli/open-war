using System;
using System.ComponentModel;
using Gameplay.Shop;
using Save;
using Unity.VisualScripting;
using UnityEngine;

namespace Shop
{
    public class Upgrade : ISaver
    {
        public readonly string name;
        public readonly string description;
        public int level;
        public readonly int maxLevel;
        public readonly int initialCost;
        public int Cost => initialCost * (int)Math.Pow(1.1, level);

        public Upgrade(string name, string description, int level, int maxLevel, int initialCost)
        {
            this.name = name;
            this.description = description;
            this.level = level;
            this.maxLevel = maxLevel;
            this.initialCost = initialCost;
        }

        public bool Available() => level < maxLevel && Cost <= Inventory.Money;

        private static string PathLevel(string path) => $"{path}/Level";

        public void Save(string path)
        {
            PlayerPrefs.SetInt(PathLevel(path), level);
        }

        public bool Load(string path)
        {
            if (!PlayerPrefs.HasKey(PathLevel(path)))
                return false;
            level = PlayerPrefs.GetInt(PathLevel(path));
            return true;
        }
    }
}