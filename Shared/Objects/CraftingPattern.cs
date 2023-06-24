using UnityEngine;

[System.Serializable]
public class ItemStack
{
    [SerializeField]
    private Item _item;
    [SerializeField]
    private int _amount;

    public Item Item => _item;

    public int Amount => _amount;
}

[CreateAssetMenu]
public class CraftingPattern : ScriptableObject
{
    [SerializeField]
    private ItemStack[] _reagents;
    [SerializeField]
    private ItemStack _outcome;

    public ItemStack[] Reagents => _reagents;

    public ItemStack Outcome => _outcome;
}
