using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotGUI : MonoBehaviour, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private ItemGUI _itemGUI;
    private bool _isHovering = false;

    public InventorySlot Slot { get; private set; }

    private void Update() {
        if (_isHovering && Input.GetMouseButtonUp(1))
            if (Slot.Item != null && Slot.Item is Weapon)
                TCPClient.Send(new MessageEquip(Slot.Item.name));
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (Slot.Item != null)
            DragAndDropGUI.Current.StartDrag(Slot.Item.Icon);
    }

    public void OnEndDrag(PointerEventData eventData) {
        DragAndDropGUI.Current.StopDrag();
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

    public void Initialize(InventorySlot slot) {
        slot.OnChanged += OnChanged;
        Slot = slot;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        _isHovering = false;
    }
}