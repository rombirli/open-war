using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class UpgradeContainer : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI priceText;
        public Slider levelSlider;

        public Upgrade upgrade
        {
            set
            {
                nameText.text = value.name;
                levelText.text = value.level.ToString();
                priceText.text = value.initialCost.ToString();
                levelSlider.maxValue = value.maxLevel;
                levelSlider.value = value.level;
            }
        }

        public void Upgrade()
        {
        }
    }
}