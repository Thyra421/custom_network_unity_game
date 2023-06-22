using System;
using System.Linq;

public class InventorySlot
{
    private Item _item;
    private int _amount;

    public void Clear() {
        _item = null;
        _amount = 0;
        OnChanged(_item, _amount);
    }

    public void Set(Item item, int amount) {
        _item = item;
        _amount = amount;
        OnChanged(_item, _amount);
    }

    public void Remove(int amount) {
        _amount -= amount;
        if (_amount == 0)
            _item = null;
        OnChanged(_item, _amount);
    }

    public void Add(int amount) {
        _amount += amount;
        OnChanged(_item, _amount);
    }

    public Item Item => _item;

    public int Amount => _amount;

    public bool IsEmpty => _item == null;

    public delegate void OnChangedHandler(Item item, int amount);

    public OnChangedHandler OnChanged;
}

public class Inventory
{
    private readonly InventorySlot[] _slots = new InventorySlot[SharedConfig.INVENTORY_SPACE];

    private InventorySlot Find(Item item) => Array.Find(_slots, (InventorySlot i) => i.Item == item);

    private bool Any(Item item) => _slots.Any((InventorySlot i) => i.Item == item);

    private InventorySlot EmptySlot => Array.Find(_slots, (InventorySlot i) => i.Item == null);

    public Inventory() {
        for (int i = 0; i < _slots.Length; i++)
            _slots[i] = new InventorySlot();
    }

    public void Add(Item item, int amount) {
        if (!Any(item) || !(item.Property == ItemProperty.Stackable))
            EmptySlot.Set(item, amount);
        else
            Find(item).Add(amount);
    }

    public void Remove(Item item, int amount) {
        Find(item).Remove(amount);
    }

    public InventorySlot[] Slots => _slots;
}