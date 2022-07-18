using System;
using Shop;
using TMPro;
using UnityEngine;

namespace Menus
{
    public class TankContainer : MonoBehaviour
    {
        public GameObject tankUnlockedPrefab;
        public GameObject tankLockedPrefab;
        public GameObject upgradesContainersParent;
        public GameObject upgradeContainerPrefab;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI priceText;

        public Tank Tank
        {
            set
            {
                tankUnlockedPrefab.SetActive(value.Owned());
                tankLockedPrefab.SetActive(!value.Owned());
                nameText.SetText(value.name);
                priceText.SetText(value.price.ToString());
                foreach (var upgrade in value.Upgrades)
                    Instantiate(upgradeContainerPrefab, upgradesContainersParent.transform)
                        .GetComponent<UpgradeContainer>().upgrade = upgrade;
            }
        }

        public void Start()
        {
        }

        public void Buy()
        {
        }
    }
}