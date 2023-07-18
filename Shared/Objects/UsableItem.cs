using System;
using UnityEngine;

public interface IItemEffectController
{
    public void RestoreHealth(int amount);
}

[Serializable]
public class ItemEffect : Effect
{
    public ItemEffect() : base(typeof(IItemEffectController).AssemblyQualifiedName) { }
}

[CreateAssetMenu]
public class UsableItem : Item, ICooldownHandler
{
    [SerializeField]
    private ItemEffect[] _effects;
    [SerializeField]
    private float _cooldown;

    public ItemEffect[] Effects => _effects;

    public float Cooldown => _cooldown;
}