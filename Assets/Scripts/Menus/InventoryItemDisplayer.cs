using Gameplay.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplayer : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public Slider slider;
    public float Max { get; set; }
    public float Value { get; set; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Value/ Max;
        textMeshPro.SetText($"{Inventory.Health}/{Inventory.MaxHealth}");
    }
}