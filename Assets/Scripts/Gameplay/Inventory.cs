﻿using System;
using System.Collections.Generic;
using System.Linq;

public static class Inventory
{
    public enum Item
    {
        Health = 0,
        MainAmmo = 1,
        TurretAmmo = 2,
        Coin = 3,
        Key = 4
    }

    public static readonly Item[] Items = (Item[])Enum.GetValues(typeof(Item));

    private static readonly Dictionary<Item, int> ItemToCount =
        LoadItemToCount();


    private static readonly Dictionary<Item, int> ItemToCapacity =
        LoadItemToCapacity();

    private static Dictionary<Item, int> LoadItemToCount() =>
        Items.ToDictionary(item => item, _ => 0);

    private static Dictionary<Item, int> LoadItemToCapacity() =>
        new Dictionary<Item, int>()
        {
            { Item.Health, 100 },
            { Item.MainAmmo, 100 },
            { Item.TurretAmmo, 10 },
            { Item.Coin, 1000 }, 
            { Item.Key, 1000 }
        };



    public static int GetCount(Item item) => ItemToCount[item];
    public static int GetCapacity(Item item) => ItemToCapacity[item];

    public static bool Put(Item item)
    {
        if (GetCount(item) < GetCapacity(item))
        {
            ItemToCount[item]++;
            return true;
        }

        return false;
    }


    public static bool Pop(Item item)
    {
        if (ItemToCount[item] > 0)
        {
            ItemToCount[item]--;
            return true;
        }

        return false;
    }

    public static void ForceSet(Item item, int count)
    {
        ItemToCount[item] = count;
    }
}