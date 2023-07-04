using UnityEngine;

public class InventoryGUI : MonoBehaviour
{
    private static InventoryGUI _current;
    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private GameObject _slotGUITemplate;

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
    }

    private void Start() {
        for (int i = 0; i < InventoryManager.Current.Slots.Length; i++) {
            InventorySlotGUI newSlot = Instantiate(_slotGUITemplate, _parent).GetComponent<InventorySlotGUI>();
            newSlot.Initialize(InventoryManager.Current.Slots[i]);
        }
    }

    public static InventoryGUI Current => _current;
}