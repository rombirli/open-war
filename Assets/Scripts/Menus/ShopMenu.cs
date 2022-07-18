using System;
using Shop;
using Unity.VisualScripting;
using UnityEngine;

namespace Menus
{
    public class ShopMenu : MonoBehaviour
    {
        public GameObject tankContainerPrefab;
        public GameObject tankContainersParent;
        public void Start()
        {
            foreach (var tank in Tank.Tanks)
                Instantiate(tankContainerPrefab, tankContainersParent.transform)
                    .GetComponent<TankContainer>().Tank = tank;
        }
    }
}