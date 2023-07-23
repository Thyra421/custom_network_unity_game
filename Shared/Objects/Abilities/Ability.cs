using UnityEngine;

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