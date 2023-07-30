using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Ability")]
public class Ability : ScriptableObject, IDisplayable, IRechargeable, IUsable, ITooltipHandler
{
    [SerializeField]
    private string _displayName;
    [SerializeField]
    private float _cooldownInSeconds;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private DirectEffect[] _effects;
    [SerializeField]
    private string _animationName;
    [TextArea(1, 5)]
    [SerializeField]
    private string _description;

    public string DisplayName => _displayName;

    public Sprite Icon => _icon;

    public DirectEffect[] Effects => _effects;

    public float CooldownInSeconds => _cooldownInSeconds;

    public string AnimationName => _animationName;

    public string Description => _description;

    public virtual void BuildTooltip(RectTransform parent) {
        TooltipBuilder.Current.BuildText(parent, _displayName);
        TooltipBuilder.Current.BuildText(parent, $"{_cooldownInSeconds} seconds cooldown");
        TooltipBuilder.Current.BuildText(parent, $"On use: {_description}", Color.green);
    }
}