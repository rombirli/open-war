using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject fullInventoryMenu;
    public GameObject smallInventoryMenu;
    public GameObject freshPlayer;
    public GameObject deadMenu;
    public GameObject pauseMenu;

    public TextMeshProUGUI deadMenuInfos;
    // public GameObject pauseMenu;
    private void Start()
    {
        GameManager.Paused = false;
    }

    // Update is called once per frame
    public void Update()
    {
        var inventoryMenuOpen = Input.GetKey(KeyCode.E);
        smallInventoryMenu.SetActive(!inventoryMenuOpen);
        fullInventoryMenu.SetActive(inventoryMenuOpen);
        deadMenu.SetActive(Inventory.GetCount(Inventory.Item.Health) <= 0);
        deadMenuInfos.text = $"Time alive : : {0}\nDistance from spawn : {0}\nDamages score : {0}";
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        GameManager.Paused=true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        GameManager.Paused=false;
    }

    public void Home()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Menu");
    }
    public void Respawn()
    {
        Inventory.ForceSet(Inventory.Item.Health, Inventory.GetCapacity(Inventory.Item.Health));
        GameObject.FindWithTag("Player").tag = "Untagged";
        Instantiate(freshPlayer, Vector3.zero, Quaternion.identity);
    }
}