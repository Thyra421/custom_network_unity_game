using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotGUI : MonoBehaviour, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInteractableGUI
{
    [SerializeField]
    private ItemGUI _itemGUI;

    public InventorySlot Slot { get; private set; }

    public void OnBeginDrag(PointerEventData eventData) {
        if (Slot.Item != null)
            DragAndDropGUIManager.Current.StartDrag(Slot.Item.Icon);
    }

    public void OnEndDrag(PointerEventData eventData) {
        DragAndDropGUIManager.Current.StopDrag();
    }

    public void OnDrag(PointerEventData eventData) {
    }

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag.TryGetComponent(out InventorySlotGUI otherSlotGUI))
            Slot.Swap(otherSlotGUI.Slot);
    }

    public void OnChanged(Item item, int amount) {
        _itemGUI.Initialize(item, amount);
    }

    public void Interact() {
        if (Slot.Item == null)
            return;

        if (Slot.Item is Weapon)
            TCPClient.Send(new MessageEquip(Slot.Item.name));
        if (Slot.Item is UsableItem)
            TCPClient.Send(new MessageUseItem(Slot.Item.name, Vector3Data.Zero));

    }

    public void Initialize(InventorySlot slot) {
        slot.OnChanged += OnChanged;
        Slot = slot;
    }
}