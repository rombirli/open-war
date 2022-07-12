using System.Collections;
using System.Collections.Generic;
using Save;
using UnityEngine;

public class InventorySaver : MonoBehaviour, ISaver
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Save(string path)
    {
        // coin
        PlayerPrefs.SetInt($"{path}/COINS", Inventory.GetCount(Inventory.Item.Coin));
        // health
        PlayerPrefs.SetInt($"{path}/HEALTH", Inventory.GetCount(Inventory.Item.Health));
        // key
        PlayerPrefs.SetInt($"{path}/KEYS", Inventory.GetCount(Inventory.Item.Key));
        // main ammo
        PlayerPrefs.SetInt($"{path}/MAIN_AMMO", Inventory.GetCount(Inventory.Item.MainAmmo));
        // turret ammo
        PlayerPrefs.SetInt($"{path}/TURRET_AMMO", Inventory.GetCount(Inventory.Item.TurretAmmo));
    }


    public bool Load(string path)
    {
        Debug.Log("Loading inventory");
        if (!PlayerPrefs.HasKey($"{path}/COINS") ||
            !PlayerPrefs.HasKey($"{path}/HEALTH") ||
            !PlayerPrefs.HasKey($"{path}/KEYS") ||
            !PlayerPrefs.HasKey($"{path}/MAIN_AMMO") ||
            !PlayerPrefs.HasKey($"{path}/TURRET_AMMO"))
            return false;
        // coin
        Inventory.ForceSet(Inventory.Item.Coin, PlayerPrefs.GetInt($"{path}/COINS"));
        // health
        Inventory.ForceSet(Inventory.Item.Health, PlayerPrefs.GetInt($"{path}/HEALTH"));
        // key
        Inventory.ForceSet(Inventory.Item.Key, PlayerPrefs.GetInt($"{path}/KEYS"));
        // main ammo
        Inventory.ForceSet(Inventory.Item.MainAmmo, PlayerPrefs.GetInt($"{path}/MAIN_AMMO"));
        // turret ammo
        Inventory.ForceSet(Inventory.Item.TurretAmmo, PlayerPrefs.GetInt($"{path}/TURRET_AMMO"));
        return true;
    }
}