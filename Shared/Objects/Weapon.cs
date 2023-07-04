using System;
using UnityEngine;

public abstract class WeaponAbilityActionController
{
    public abstract void Melee(string animationName);

    public abstract void Projectile(GameObject prefab, float speed, float distance);
}

[Serializable]
public class WeaponAbilityAction : Action
{
    public WeaponAbilityAction() : base(typeof(WeaponAbilityActionController).AssemblyQualifiedName) { }
}

[Serializable]
public class WeaponAbility : ScriptableObject
{
    [SerializeField]
    private string _displayName;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private float _damages;    
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private WeaponAbilityAction[] _actions;

    public WeaponAbilityAction[] Actions => _actions;
}

[CreateAssetMenu]
public class Weapon : Item
{
    [SerializeField]
    private WeaponAbility[] _abilities;

    public WeaponAbility[] Abilities => _abilities;
}