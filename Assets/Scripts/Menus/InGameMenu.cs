using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject fullInventoryMenu;
    public GameObject smallInventoryMenu;
    public GameObject freshPlayer;
    public GameObject handedInputs;

    public GameObject deadMenu;
    public GameObject pauseMenu;

    public TextMeshProUGUI deadMenuInfos;
    private PlayerInput _input;

    // public GameObject pauseMenu;
    private void Start()
    {
        GameManager.Paused = false;
        _input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    public void Update()
    {
        var inventoryMenuOpen = _input.actions["OpenFullInventory"].inProgress;
        smallInventoryMenu.SetActive(!inventoryMenuOpen);
        fullInventoryMenu.SetActive(inventoryMenuOpen);
        deadMenu.SetActive(Inventory.Health <= 0);
        deadMenuInfos.text = $"Time alive : : {0}\nDistance from spawn : {0}\nDamages score : {0}";
        if (_input.actions["Pause"].triggered)
            Pause();
    }

    public void Pause()
    {
        // handedInputs.SetActive(false);
        pauseMenu.SetActive(true);
        GameManager.Paused = true;
    }

    public void Resume()
    {
        // handedInputs.SetActive(true);
        pauseMenu.SetActive(false);
        GameManager.Paused = false;
    }

    public void Home()
    {
        GameManager.Paused = false;
        SceneManager.LoadScene("Menu");
    }

    public void Respawn()
    {
        Inventory.Health = Inventory.MaxHealth;
        GameObject.FindWithTag("Player").tag = "Untagged";
        Instantiate(freshPlayer, Vector3.zero, Quaternion.identity);
    }
}