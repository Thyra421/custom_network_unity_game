public class InventorySlot
{
    private Item _item;
    private int _amount;
    private event OnChangedHandler _onChanged;

    public void Clear() {
        _item = null;
        _amount = 0;
        _onChanged(_item, _amount);
    }

    public void Set(Item item, int amount) {
        _item = item;
        _amount = amount;
        _onChanged(_item, _amount);
    }

    public void Remove(int amount) {
        _amount -= amount;
        if (_amount == 0)
            _item = null;
        _onChanged(_item, _amount);
    }

    public void Add(int amount) {
        _amount += amount;
        _onChanged(_item, _amount);
    }

    public Item Item => _item;

    public int Amount => _amount;

    public bool IsEmpty => _item == null;

    public delegate void OnChangedHandler(Item item, int amount);

    public event OnChangedHandler OnChanged {
        add => _onChanged += value;
        remove => _onChanged -= value;
    }
}

