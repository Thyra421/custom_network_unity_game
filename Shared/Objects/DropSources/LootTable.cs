using System.Linq;
using UnityEngine;

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
