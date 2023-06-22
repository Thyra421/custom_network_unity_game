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

[System.Serializable]
public class LootTable
{
    [SerializeField]
    private LootTableEntry[] _entries;

    public LootTableEntry[] Entries => _entries;
}

[CreateAssetMenu(menuName = "Drop Source")]
public class DropSource : ScriptableObject
{
    [SerializeField]
    private LootTable _lootTable;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private int _minInclusive;
    [SerializeField]
    private int _minExclusive;

    public Item RandomLoot => Utils.PickRandomItem(_lootTable);

    public GameObject Prefab => _prefab;

    public LootTable Drops => _lootTable;

    public int MinInclusive=> _minInclusive;

    public int MaxExclusive => _minExclusive;
}