using UnityEngine.EventSystems;
using UnityEngine;

enum ActionBarSlotContent
{
    None, Ability, Item
}

public class ActionBarSlotGUI : MonoBehaviour, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private ItemGUI _itemGUI;
    private ActionBarSlotContent _content;

    private void Transfert(ActionBarSlotGUI other) {
        switch (other._content) {
            case ActionBarSlotContent.None:
                Clear();
                break;
            case ActionBarSlotContent.Ability:
                break;
            case ActionBarSlotContent.Item:
                InitializeItem(other._itemGUI.Item, other._itemGUI.Amount);
                break;
        }
    }

    private void Clear() {
        _itemGUI.Initialize(null, 0);
    }

    private void SwapItem(ActionBarSlotGUI other) {
        Item tmpItem = other._itemGUI.Item;
        int tmpAmount = other._itemGUI.Amount;
        other.Transfert(this);
        InitializeItem(tmpItem, tmpAmount);
    }

    private void Swap(ActionBarSlotGUI other) {
        switch (other._content) {
            case ActionBarSlotContent.Ability:
                break;
            case ActionBarSlotContent.Item:
                SwapItem(other);
                break;
        }
    }

    public void InitializeItem(Item item, int amount) {
        _content = ActionBarSlotContent.Item;
        _itemGUI.Initialize(item, amount);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (_itemGUI.Item != null)
            DragAndDropGUI.Current.StartDrag(_itemGUI.Item.Icon);
    }

    public void OnDrag(PointerEventData eventData) {
    }

    public void OnEndDrag(PointerEventData eventData) {
        DragAndDropGUI.Current.StopDrag();
    }

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag.TryGetComponent(out ActionBarSlotGUI actionBarSlotGUI) && actionBarSlotGUI._itemGUI.Item != null)
            Swap(actionBarSlotGUI);
        if (eventData.pointerDrag.TryGetComponent(out InventorySlotGUI inventorySlotGUI) && inventorySlotGUI.Slot.Item != null)
            InitializeItem(inventorySlotGUI.Slot.Item, inventorySlotGUI.Slot.Amount);
    }
}