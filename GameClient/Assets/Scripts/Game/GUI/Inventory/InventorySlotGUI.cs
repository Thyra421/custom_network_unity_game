using UnityEngine;

public class InventorySlotGUI : MonoBehaviour, IDropAreaGUI, IDraggableGUI, IInteractableGUI
{
    [SerializeField]
    private ItemGUI _itemGUI;

    public InventorySlot Slot { get; private set; }

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

    public void OnDrop(IDraggableGUI draggable) {
        if (draggable is InventorySlotGUI otherSlot)
            Slot.Swap(otherSlot.Slot);
    }

    public void Initialize(InventorySlot slot) {
        slot.OnChanged += OnChanged;
        Slot = slot;
    }

    public void Discard() { }

    public Sprite DragIndicator => Slot.Item.Icon;

    public bool IsReadyToBeDragged => Slot.Item != null;
}