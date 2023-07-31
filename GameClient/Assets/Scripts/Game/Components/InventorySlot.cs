public class InventorySlot
{
    public Item Item { get; private set; }
    public int Amount { get; private set; }

    public delegate void OnChangedHandler(Item item, int amount);
    public event OnChangedHandler OnChanged;

    public bool IsEmpty => Item == null;

    public void Clear() {
        Item = null;
        Amount = 0;
        OnChanged?.Invoke(Item, Amount);
    }

    public void Set(Item item, int amount) {
        Item = item;
        Amount = amount;
        OnChanged?.Invoke(Item, Amount);
    }

    public void Swap(InventorySlot other) {
        Item tmpItem = other.Item;
        int tmpAmount = other.Amount;

        other.Set(Item, Amount);
        Set(tmpItem, tmpAmount);
    }

    public void Remove(int amount) {
        Amount -= amount;
        if (Amount == 0)
            Item = null;
        OnChanged?.Invoke(Item, Amount);
    }

    public void Add(int amount) {
        Amount += amount;
        OnChanged?.Invoke(Item, Amount);
    }
}

