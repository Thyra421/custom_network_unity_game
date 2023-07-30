using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Usable")]
public class UsableItem : Item, IRechargeable, IUsable
{
    [SerializeField]
    private DirectEffect[] _effects;
    [SerializeField]
    private float _cooldownInSeconds;

    public DirectEffect[] Effects => _effects;

    public float CooldownInSeconds => _cooldownInSeconds;

    public override void BuildTooltip(RectTransform parent) {
        base.BuildTooltip(parent);
        TooltipBuilder.Current.BuildText(parent, $"{_cooldownInSeconds} seconds cooldown");
        foreach (DirectEffect effect in _effects)
            TooltipBuilder.Current.BuildText(parent, effect.MethodName);
    }
}