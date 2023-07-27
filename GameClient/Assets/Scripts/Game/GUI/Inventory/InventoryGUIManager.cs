using UnityEngine;

public class InventoryGUIManager : MonoBehaviour
{
    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private GameObject _slotGUITemplate;

    public static InventoryGUIManager Current { get; private set; }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
    }

    private void Start() {
        for (int i = 0; i < InventoryManager.Current.Slots.Length; i++) {
            InventorySlotGUI newSlot = Instantiate(_slotGUITemplate, _parent).GetComponent<InventorySlotGUI>();
            newSlot.Initialize(InventoryManager.Current.Slots[i]);
        }
    }
}