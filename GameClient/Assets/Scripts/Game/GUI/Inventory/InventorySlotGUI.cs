using UnityEngine;

public class InventorySlotGUI : MonoBehaviour
{
    [SerializeField]
    private ItemGUI _itemGUI;

    public void OnChanged(Item item, int amount) {
        _itemGUI.Initialize(item, amount);
    }
}