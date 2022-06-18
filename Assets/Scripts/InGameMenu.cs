using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject fullInventoryMenu;
    public GameObject smallInventoryMenu;
    public GameObject freshPlayer;
    public GameObject deadMenu;
    // public GameObject pauseMenu;

    // Update is called once per frame
    public void Update()
    {
        var inventoryMenuOpen = Input.GetKey(KeyCode.E);
        smallInventoryMenu.SetActive(!inventoryMenuOpen);
        fullInventoryMenu.SetActive(inventoryMenuOpen);
        deadMenu.SetActive(Inventory.GetCount(Inventory.Item.Health) <= 0);
    }

    public void Respawn()
    {
        Inventory.ResetInventory();
        GameObject.FindWithTag("Player").tag = "Untagged";
        Instantiate(freshPlayer, Vector3.zero, Quaternion.identity);
        GameManager.Resume();
    }
}