using UnityEngine;

public enum AbilityTargetingType
{
    OnPlayer, Aimed, GroundedArea
}

[CreateAssetMenu]
public class Ability : ScriptableObject, IRechargeable, IUsable
{
    [SerializeField]
    private string _displayName;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private AbilityTargetingType _targetingType;
    [SerializeField]
    private Effect[] _effects;

    public Effect[] Effects => _effects;

    public Sprite Icon => _icon;

    public float Cooldown => _cooldown;

    public AbilityTargetingType TargetingType => _targetingType;
}