using GameActors;
using Gameplay.Shop;
using Save;
using UnityEngine;

namespace Shop
{
    public class Purchasable : ISaver
    {
        public readonly string name;
        public readonly int price;
        public int availableQuantity;
        public int ownedQuantity;

        public Purchasable(string name, int price)
        {
            this.name = name;
            this.price = price;
            availableQuantity = 1;
            ownedQuantity = 0;
        }

        public Purchasable(string name, int price, int availableQuantity, int ownedQuantity)
        {
            this.name = name;
            this.price = price;
            this.availableQuantity = availableQuantity;
            this.ownedQuantity = ownedQuantity;
        }

        public bool Available(int n = 1) =>
            Inventory.Money * n >= price && availableQuantity > 0;

        public bool Buy(int n = 1)
        {
            if (!Available(n)) return false;
            availableQuantity -= n;
            ownedQuantity += n;
            return true;
        }


        private static string PathOwnedQuantity(string path) => $"{path}/OwnedQuantity";
        private static string PathAvailableQuantity(string path) => $"{path}/AvailableQuantity";
        
        public virtual void Save(string path)
        {
            PlayerPrefs.SetInt(PathOwnedQuantity(path), ownedQuantity);
            PlayerPrefs.SetInt(PathAvailableQuantity(path), availableQuantity);
        }
        public virtual bool Load(string path)
        {
            if (!PlayerPrefs.HasKey(PathOwnedQuantity(path)) ||
                !PlayerPrefs.HasKey(PathAvailableQuantity(path)))
                return false;
            ownedQuantity = PlayerPrefs.GetInt(PathOwnedQuantity(path), 0);
            availableQuantity = PlayerPrefs.GetInt(PathAvailableQuantity(path), 0);
            return true;
        }
    }
}