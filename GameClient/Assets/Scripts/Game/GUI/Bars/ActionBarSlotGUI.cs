using UnityEngine;

public class ActionBarSlotGUI : MonoBehaviour, IDropAreaGUI, IDraggableGUI
{
    [SerializeField]
    private ItemGUI _itemGUI;

    public Sprite DragIndicator => _itemGUI.Item.Icon;
    public bool IsReadyToBeDragged => _itemGUI.Item != null;

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

    public void OnDrop(IDraggableGUI draggable) {
        if (draggable is ActionBarSlotGUI actionBarSlotGUI)
            Swap(actionBarSlotGUI);
        else if (draggable is InventorySlotGUI inventorySlotGUI)
            Initialize(inventorySlotGUI.Slot.Item, inventorySlotGUI.Slot.Amount);
    }

    public void Use() {
        if (_itemGUI.Item == null || !StatesManager.Current.HasControl)
            return;

        if (_itemGUI.Item is Weapon)
            TCPClient.Send(new MessageEquip(_itemGUI.Item.name));
        if (_itemGUI.Item is UsableItem)
            TCPClient.Send(new MessageUseItem(_itemGUI.Item.name, Vector3Data.Zero));
    }

    public void Discard() {
        Clear();
    }    
}