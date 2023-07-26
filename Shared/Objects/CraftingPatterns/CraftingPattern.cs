using UnityEngine;

[CreateAssetMenu]
public class CraftingPattern : ScriptableObject
{
    [SerializeField]
    private ItemStack[] _reagents;
    [SerializeField]
    private ItemStack _outcome;
    [SerializeField]
    private ExperienceType _experienceType;

    public ItemStack[] Reagents => _reagents;

    public ItemStack Outcome => _outcome;

    public ExperienceType ExperienceType => _experienceType;
}
