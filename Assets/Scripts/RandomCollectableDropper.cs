using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomCollectableDropper : MonoBehaviour
{
    [System.Serializable]
    public struct ItemWithWeight
    {
        public GameObject item;
        public float weight;
    }

    public List<ItemWithWeight> itemsWithWeight;

    private bool spawned = false;
    // Start is called before the first frame update

    private void OnEnable()
    {
        if (spawned) return;
        Instantiate(GetRandomItem(), transform.position, Quaternion.identity);
        spawned = true;
    }

    private GameObject GetRandomItem()
    {
        var weightsSum = itemsWithWeight.Sum(itemWithWeight => itemWithWeight.weight);
        var selected = Random.Range(0, weightsSum);
        float localSum = 0;
        foreach (var itemWithWeight in itemsWithWeight)
        {
            if (localSum <= selected && selected <= localSum + itemWithWeight.weight)
                return itemWithWeight.item;
            localSum += itemWithWeight.weight;
        }

        return null;
    }
}