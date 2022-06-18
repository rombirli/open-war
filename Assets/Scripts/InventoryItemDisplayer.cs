using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplayer : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public Slider slider;

    public Inventory.Item item;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Inventory.GetCount(item) / (float)Inventory.GetCapacity(item);
        textMeshPro.SetText($"{Inventory.GetCount(item)}/{Inventory.GetCapacity(item)}");
    }
}