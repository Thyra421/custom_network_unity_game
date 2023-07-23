using UnityEngine;

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
