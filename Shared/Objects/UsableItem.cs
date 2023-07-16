using System;
using UnityEngine;

public interface IItemActionController
{
    public void RestoreHealth(int amount);
}

[Serializable]
public class ItemAction : Action
{
    public ItemAction() : base(typeof(IItemActionController).AssemblyQualifiedName) { }
}

[CreateAssetMenu]
public class UsableItem : Item, ICooldownHandler
{
    [SerializeField]
    private ItemAction[] _actions;
    [SerializeField]
    private float _cooldown;

    public ItemAction[] Actions => _actions;

    public float Cooldown => _cooldown;
}