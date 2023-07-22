using System;
using UnityEngine;

public enum AbilityTargetingType
{
    Melee, Aimed, GroundedArea
}

[Serializable]
public class AbilityHit : IUsable
{
    [SerializeField]
    private Effect[] _effects;

    public Effect[] Effects => _effects;
}

public abstract class Ability : ScriptableObject, IRechargeable, IUsable
{
    [SerializeField]
    private string _displayName;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private Effect[] _effects;
    [SerializeField]
    private string _animationName;

    public Effect[] Effects => _effects;

    public Sprite Icon => _icon;

    public float Cooldown => _cooldown;

    public string AnimationName => _animationName;
}

public abstract class OffensiveAbility : Ability
{
    [SerializeField]
    private AbilityHit _hit;

    public AbilityHit Hit => _hit;
}