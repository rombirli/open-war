using System;
using GameActors;
using JetBrains.Annotations;
using Save;
using Shop;
using UnityEngine;

namespace Gameplay.Shop
{
    public class InventorySaver : MonoBehaviour, ISaver
    {
        private static string PathMoney(string path) => $"{path}/Money";
        private static string PathGems(string path) => $"{path}/Gems";
        private static string PathCurrentTank(string path) => $"{path}/CurrentTank";
        private static string PathCurrentTurret(string path) => $"{path}/CurrentTurret";
        private static string PathTurret(string path, Turret turret) => $"{path}/Turrets/{turret.name}";
        private static string PathTank(string path, Tank tank) => $"{path}/Tanks/{tank.name}";

        private void Update()
        {
        }

        public void Save(string path)
        {
            // save turrets
            foreach (var turret in Turret.Turrets)
            {
                turret.Save(PathTurret(path, turret));
            }

            // save tanks   
            foreach (var tank in Tank.Tanks)
            {
                tank.Save(PathTank(path, tank));
            }

            // save consumables
            Bag.Save(path);

            // save money
            PlayerPrefs.SetInt(PathMoney(path), Inventory.Money);
            // save gems
            PlayerPrefs.SetInt(PathGems(path), Inventory.Gems);
            // save current tank
            PlayerPrefs.SetString(PathCurrentTank(path), Tank.CurrentTank.name);
            // save current turret
            PlayerPrefs.SetString(PathCurrentTurret(path), Turret.CurrentTurret.name);
        }


        public bool Load(string path)
        {
            // load turrets
            foreach (var turret in Turret.Turrets)
            {
                if (!turret.Load(PathTurret(path, turret))) return false;
            }

            // load tanks
            foreach (var tank in Tank.Tanks)
            {
                if (!tank.Load(PathTank(path, tank))) return false;
            }

            // load consumables
            if (!Bag.Load(path)) return false;

            if (!PlayerPrefs.HasKey(PathMoney(path)) ||
                !PlayerPrefs.HasKey(PathGems(path)) ||
                !PlayerPrefs.HasKey(PathCurrentTank(path)) ||
                !PlayerPrefs.HasKey(PathCurrentTurret(path))) return false;
            // load money
            Inventory.Money = PlayerPrefs.GetInt(PathMoney(path));
            // load gems
            Inventory.Gems = PlayerPrefs.GetInt(PathGems(path));
            // load current tank
            Tank.CurrentTank = Array.Find(Tank.Tanks, t => t.name == PlayerPrefs.GetString(PathCurrentTank(path)));
            // load current turret
            Turret.CurrentTurret =
                Array.Find(Turret.Turrets, t => t.name == PlayerPrefs.GetString(PathCurrentTurret(path)));
            return true;
        }
    }
}