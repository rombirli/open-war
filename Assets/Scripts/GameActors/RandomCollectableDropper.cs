using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Save;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomCollectableDropper : MonoBehaviour, ISaver
{
    [System.Serializable]
    public struct ItemWithWeight
    {
        public GameObject item;
        public float weight;
    }

    public List<ItemWithWeight> itemsWithWeight;
    private GameObject _instantiated;
    private int _selected = -1;


    // Start is called before the first frame update

    private void Start()
    {
        if (_selected < 0)
            Instantiate();
    }

    private void Instantiate()
    {
        var (item, index) = GetRandomItem();
        _selected = index;
        _instantiated = Instantiate(item, transform);
    }

    private Tuple<GameObject, int> GetRandomItem()
    {
        var weightsSum = itemsWithWeight.Sum(itemWithWeight => itemWithWeight.weight);
        var selected = Random.Range(0, weightsSum);
        float localSum = 0;
        var i = 0;
        foreach (var itemWithWeight in itemsWithWeight)
        {
            if (localSum <= selected && selected <= localSum + itemWithWeight.weight)
                return Tuple.Create(itemWithWeight.item, i);
            localSum += itemWithWeight.weight;
            i++;
        }

        return Tuple.Create<GameObject, int>(null, -1);
    }

    public void Save(string path)
    {
        if (_instantiated)
            PlayerPrefs.SetInt(path, _selected);
        else PlayerPrefs.SetInt(path, -1);
    }

    public bool Load(string path)
    {
        if (!PlayerPrefs.HasKey(path)) return false;
        Destroy(_instantiated);
        _selected = PlayerPrefs.GetInt(path);
        var i = 0;
        foreach (var itemWithWeight in itemsWithWeight)
            if (i++ == _selected)
                _instantiated = Instantiate(itemWithWeight.item, transform);

        return true;
    }
}