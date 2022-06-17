using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Collectable : MonoBehaviour
{
    public Inventory.Item item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Inventory.Put(item))
            Destroy(gameObject);
    }
}