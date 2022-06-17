using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject fullInventoryMenu;
    public GameObject smallInventoryMenu;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var inventoryMenuOpen = Input.GetKey(KeyCode.E);
        smallInventoryMenu.SetActive(!inventoryMenuOpen);
        fullInventoryMenu.SetActive(inventoryMenuOpen);
    }
}