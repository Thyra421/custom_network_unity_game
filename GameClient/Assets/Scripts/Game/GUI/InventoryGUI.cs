using UnityEngine;

public class InventoryGUI : MonoBehaviour
{
    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private InventoryManager _inventoryManager;
    [SerializeField]
    private GameObject _slotGUITemplate;

    private void Start() {
        for (int i = 0; i < _inventoryManager.Slots.Length; i++) {
            InventorySlotGUI newSlot = Instantiate(_slotGUITemplate, _parent).GetComponent<InventorySlotGUI>();
            _inventoryManager.Slots[i].OnChanged += newSlot.OnChanged;
        }
    }
}