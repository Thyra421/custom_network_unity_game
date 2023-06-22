using UnityEngine;

public class InventoryGUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _slotTemplate;
    [SerializeField]
    private LocalPlayer _player;

    private void Start() {
        for (int i = 0; i < SharedConfig.INVENTORY_SPACE; i++) {
            InventorySlotGUI newSlot = Instantiate(_slotTemplate, transform).GetComponent<InventorySlotGUI>();
            _player.Inventory.Slots[i].OnChanged += newSlot.OnChanged;
        }
    }
}