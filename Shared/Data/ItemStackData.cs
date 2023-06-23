public struct ItemStackData
{
    public string itemName;
    public int amount;

    public ItemStackData(string itemName, int amount) {
        this.itemName = itemName;
        this.amount = amount;
    }
}