using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Collectable : MonoBehaviour
{
    public Inventory.Item item;
    public int count = 1;

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
                    Destroy(gameObject);
                }
            }
            else
            {
                for (var i = 0; i < -count; i++)
                    Inventory.Pop(item);
                Destroy(gameObject);
            }
        }
    }
}