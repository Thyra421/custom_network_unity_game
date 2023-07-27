using UnityEngine.EventSystems;
using UnityEngine;

public class ActionBarSlotGUI : MonoBehaviour, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private ItemGUI _itemGUI;

    private void Clear() {
        _itemGUI.Initialize(null, 0);
    }

    private void Swap(ActionBarSlotGUI other) {
        Item tmpItem = other._itemGUI.Item;
        int tmpAmount = other._itemGUI.Amount;
        other.Initialize(_itemGUI.Item, _itemGUI.Amount);
        Initialize(tmpItem, tmpAmount);
    }

    private void OnChanged(Item item, int amount) {
        if (item == _itemGUI.Item)
            _itemGUI.Initialize(item, amount);
    }

    private void Awake() {
        InventoryManager.Current.OnChanged += OnChanged;
    }

    public void Initialize(Item item, int amount) {
        _itemGUI.Initialize(item, amount);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (_itemGUI.Item != null)
            DragAndDropGUIManager.Current.StartDrag(_itemGUI.Item.Icon);
    }

    public void OnDrag(PointerEventData eventData) {
    }

    public void OnEndDrag(PointerEventData eventData) {
        DragAndDropGUIManager.Current.StopDrag();
        if (!eventData.pointerCurrentRaycast.isValid)
            Clear();
    }

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag.TryGetComponent(out ActionBarSlotGUI actionBarSlotGUI) && actionBarSlotGUI._itemGUI.Item != null)
            Swap(actionBarSlotGUI);
        if (eventData.pointerDrag.TryGetComponent(out InventorySlotGUI inventorySlotGUI) && inventorySlotGUI.Slot.Item != null)
            Initialize(inventorySlotGUI.Slot.Item, inventorySlotGUI.Slot.Amount);
    }
}