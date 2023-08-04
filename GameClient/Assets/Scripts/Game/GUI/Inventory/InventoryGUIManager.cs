using UnityEngine;

public class InventoryGUIManager : MonoBehaviour
{
    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private GameObject _slotGUITemplate;

    private void Start() {
        for (int i = 0; i < InventoryManager.Current.Slots.Length; i++) {
            InventorySlotGUI newSlot = Instantiate(_slotGUITemplate, _parent).GetComponent<InventorySlotGUI>();
            newSlot.Initialize(InventoryManager.Current.Slots[i]);
        }
    }
}