using System;
using UnityEngine;

public interface IAbilityActionController
{
    public abstract void Melee(int damage, string animationName);

    public abstract void Projectile(int damage, GameObject prefab, float speed, float distance);
}

[Serializable]
public class AbilityAction : Action
{
    public AbilityAction() : base(typeof(IAbilityActionController).AssemblyQualifiedName) { }
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
    private AbilityAction[] _actions;

    public AbilityAction[] Actions => _actions;

    public Sprite Icon => _icon;

    public float Cooldown => _cooldown;
}