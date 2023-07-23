using UnityEngine;

[System.Serializable]
public class LootTableEntry
{
    [SerializeField]
    private Item _item;
    [SerializeField]
    private int _dropChance;

    public int DropChance => _dropChance;

    public Item Item => _item;
}
