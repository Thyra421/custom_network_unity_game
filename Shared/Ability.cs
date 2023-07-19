using System;
using UnityEngine;

public interface IAbilityEffectController
{
    public abstract void Melee(int damage, string animationName, float duration);

    public abstract void Projectile(int damage, GameObject prefab, float speed, float distance);
}

[Serializable]
public class AbilityEffect : Effect
{
    public AbilityEffect() : base(typeof(IAbilityEffectController).AssemblyQualifiedName) { }
}

[CreateAssetMenu]
public class Ability : ScriptableObject, ICooldownHandler
{
    [SerializeField]
    private string _displayName;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private AbilityEffect[] _effects;

    public AbilityEffect[] Effects => _effects;

    public Sprite Icon => _icon;

    public float Cooldown => _cooldown;
}