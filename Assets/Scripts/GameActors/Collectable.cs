using System;
using System.Collections;
using System.Collections.Generic;
using Save;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Collectable : MonoBehaviour, ISaver
{
    public Inventory.Item item;
    public int count = 1;
    private bool _collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (count > 0)
            {
                if (Inventory.GetCapacity(item) > Inventory.GetCount(item))
                {
                    for (var i = 0; i < count; i++)
                        Inventory.Put(item);
                    Collect();
                }
            }
            else
            {
                for (var i = 0; i < -count; i++)
                    Inventory.Pop(item);
                Collect();
            }
        }
    }

    private void Collect()
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
            Collect();
        return true;
    }
}