using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class Weapon : Item
{
    [SerializeField]
    private Ability[] _abilities;

    public Ability[] Abilities => _abilities;

    public override void BuildTooltip(RectTransform parent) {
        base.BuildTooltip(parent);

        foreach (Ability ability in _abilities)
            ability.BuildTooltip(parent);
    }
}