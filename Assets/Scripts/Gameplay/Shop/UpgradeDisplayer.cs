using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class UpgradeDisplayer : MonoBehaviour
    {
        
        public Upgrade upgrade;

        public TextMeshProUGUI LevelText;
        public TextMeshProUGUI UpgradeCostText;
        public Slider Slider;
        private void Update()
        {
            Slider.minValue = 0;
            Slider.maxValue = upgrade.maxLevel;
            Slider.value = upgrade.level;
            LevelText.text = $"{upgrade.level.ToString()}/{upgrade.maxLevel.ToString()}";
            UpgradeCostText.text = upgrade.Cost.ToString();
            
        }

        public void UpgradeButtonClick()
        {
            
        }
    }
}