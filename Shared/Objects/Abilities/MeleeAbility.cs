using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Melee")]
public class MeleeAbility : OffensiveAbility
{
    [SerializeField]
    private float _duration;

    public float Duration => _duration;

    public override void BuildTooltip(RectTransform parent) {
        base.BuildTooltip(parent);
        TooltipBuilder.BuildText(parent, "Melee");
    }
}
