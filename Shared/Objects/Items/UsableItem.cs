using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Usable")]
public class UsableItem : Item, IRechargeable, IUsable
{
    [SerializeField]
    private DirectEffect[] _effects;
    [SerializeField]
    private float _cooldown;

    public DirectEffect[] Effects => _effects;

    public float Cooldown => _cooldown;

    public override void BuildTooltip(RectTransform parent) {
        base.BuildTooltip(parent);
        TooltipBuilder.BuildText(parent, $"{_cooldown} seconds cooldown");
        foreach (DirectEffect effect in _effects)
            TooltipBuilder.BuildText(parent, effect.MethodName);
    }
}