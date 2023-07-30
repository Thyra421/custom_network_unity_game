using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Melee")]
public class MeleeAbility : DirectAbility
{
    [SerializeField]
    private float _durationInSeconds;

    public float DurationInSeconds => _durationInSeconds;

    public override void BuildTooltip(RectTransform parent) {
        base.BuildTooltip(parent);
        TooltipBuilder.Current.BuildText(parent, "Melee");
    }
}
