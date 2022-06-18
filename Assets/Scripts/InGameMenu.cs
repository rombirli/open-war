using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject fullInventoryMenu;
    public GameObject smallInventoryMenu;

    // Update is called once per frame
    public void Update()
    {
        var inventoryMenuOpen = Input.GetKey(KeyCode.E);
        smallInventoryMenu.SetActive(!inventoryMenuOpen);
        fullInventoryMenu.SetActive(inventoryMenuOpen);
    }
}