using System.Linq;
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

    public Item RandomLoot {
        get {
            int totalDropChance = _entries.Select((LootTableEntry e) => e.DropChance).Sum();
            int randomValue = Random.Range(0, totalDropChance);

            int cpt = 0;
            foreach (LootTableEntry e in _entries) {
                if (randomValue >= cpt && randomValue < cpt + e.DropChance)
                    return e.Item;
                cpt += e.DropChance;
            }
            return null;
        }
    }
}

[CreateAssetMenu]
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
    /// <summary>
    /// Set to -1 if never respawn.
    /// </summary>
    [SerializeField]
    private float _respawnTimerInSeconds;

    public Item RandomLoot => _lootTable.RandomLoot;

    public GameObject Prefab => _prefab;

    public LootTable Drops => _lootTable;

    public int MinInclusive => _minInclusive;

    public int MaxExclusive => _minExclusive;

    public float RespawnTimerInSeconds => _respawnTimerInSeconds;
}