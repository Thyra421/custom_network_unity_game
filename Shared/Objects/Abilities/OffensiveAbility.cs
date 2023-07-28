using UnityEngine;

public abstract class OffensiveAbility : Ability
{
    [SerializeField]
    private AbilityHit _hit;

    public AbilityHit Hit => _hit;

    public override void BuildTooltip(RectTransform parent) {
        base.BuildTooltip(parent);
        TooltipBuilder.Current.BuildText(parent, "On hit:");
        foreach (DirectEffect effect in _hit.Effects)
            TooltipBuilder.Current.BuildText(parent, effect.MethodName);
    }
}