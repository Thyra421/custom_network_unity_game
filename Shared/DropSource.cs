using System.Collections.Generic;
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
    private List<LootTableEntry> _entries;

    public List<LootTableEntry> Entries => _entries;
}

[CreateAssetMenu(menuName = "Drop Source")]
public class DropSource : ScriptableObject
{
    [SerializeField]
    private LootTable _lootTable;
    [SerializeField]
    private GameObject _prefab;

    public GameObject Prefab => _prefab;

    public LootTable Drops => _lootTable;
}