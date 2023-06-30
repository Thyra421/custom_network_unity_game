using System;
using UnityEngine;

public abstract class WeaponAbilityActionController
{
    public abstract void Frontal(GameObject colliderPrefab);

    public abstract void Projectile(GameObject colliderPrefab, float speed, float distance);

    public abstract void GroundEffect(GameObject colliderPrefab, float minRange, float maxRange);
}

[Serializable]
public class WeaponAbilityAction : Action
{
    public WeaponAbilityAction() : base(typeof(WeaponAbilityActionController).AssemblyQualifiedName) { }
}

[Serializable]
public class WeaponAbility
{
    [SerializeField]
    private WeaponAbilityAction[] _actions;
}

[CreateAssetMenu]
public class Weapon : Item
{
    [SerializeField]
    private WeaponAbility[] _abilities;

    public WeaponAbility[] Abilities => _abilities;
}