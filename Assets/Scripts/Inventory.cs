using System;
using System.Collections.Generic;
using System.Linq;

public static class Inventory
{
    public enum Item
    {
        Coin,
        Star,
        Key,
        Health,
        Freeze,
        Trap
    }

    private static readonly Dictionary<Item, int> ItemToCount =
        LoadItemToCount();


    private static readonly Dictionary<Item, int> ItemToCapacity =
        LoadItemToCapacity();

    private static Dictionary<Item, int> LoadItemToCount() =>
        ((int[])Enum.GetValues(typeof(Item))).ToDictionary(item => (Item)item, item => 0);

    private static Dictionary<Item, int> LoadItemToCapacity() =>
        ((int[])Enum.GetValues(typeof(Item))).ToDictionary(item => (Item)item, item => 100);


    public static int GetCount(Item item) => ItemToCount[item];
    public static int GetCapacity(Item item) => ItemToCapacity[item];

    public static bool Put(Item item)
    {
        if (GetCount(item) < GetCapacity(item))
        {
            ItemToCount[item]++;
            return true;
        }
        else return false;
    }


    public static bool Pop(Item item)
    {
        if (ItemToCount[item] > 0)
        {
            ItemToCount[item]--;
            return true;
        }
        else return false;
    }
}