using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Shop;
using Save;
using Shop;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Collectable : MonoBehaviour, ISaver
{
    public Consumable item;
    public int count = 1;
    private bool _collected = false;
    public UnityEvent onCollect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onCollect.Invoke();
            MarkAsCollected();
        }
    }

    private void MarkAsCollected()
    {
        _collected = true;
        foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.enabled = false;
        }

        GetComponent<Collider2D>().enabled = false;
    }

    public void Save(string path)
    {
        PlayerPrefs.SetInt(path, _collected ? 1 : 0);
    }

    public bool Load(string path)
    {
        if (!PlayerPrefs.HasKey(path))
            return false;
        if (PlayerPrefs.GetInt(path) == 1)
            MarkAsCollected();
        return true;
    }
}