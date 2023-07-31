public class InventoryItemStack
{
    public Item Item { get; }
    public int Amount { get; private set; }

    public InventoryItemStack(Item item, int amount) {
        Item = item;
        Amount = amount;
    }

    public void Remove(int amount) {
        Amount -= amount;
    }

    public void Add(int amount) {
        Amount += amount;
    }
}
