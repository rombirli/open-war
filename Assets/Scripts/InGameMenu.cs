using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject fullInventoryMenu;
    public GameObject smallInventoryMenu;
    public GameObject freshPlayer;
    public GameObject deadMenu;

    public TextMeshProUGUI deadMenuInfos;
    // public GameObject pauseMenu;

    // Update is called once per frame
    public void Update()
    {
        var inventoryMenuOpen = Input.GetKey(KeyCode.E);
        smallInventoryMenu.SetActive(!inventoryMenuOpen);
        fullInventoryMenu.SetActive(inventoryMenuOpen);
        deadMenu.SetActive(Inventory.GetCount(Inventory.Item.Health) <= 0);
        deadMenuInfos.text = $"Time alive : : {0}\nDistance from spawn : {0}\nDamages score : {0}";
    }

    public void Respawn()
    {
        Inventory.ForceSet(Inventory.Item.Health, Inventory.GetCapacity(Inventory.Item.Health));
        GameObject.FindWithTag("Player").tag = "Untagged";
        Instantiate(freshPlayer, Vector3.zero, Quaternion.identity);
    }
}