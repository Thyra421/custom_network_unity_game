using System;
using UnityEngine;

public abstract class ItemActionController
{
    public abstract void RestoreHealth(int amount);
}

[Serializable]
public class ItemAction : Action
{
    public ItemAction() : base(typeof(ItemActionController).AssemblyQualifiedName) { }
}

[CreateAssetMenu]
public class UsableItem : Item
{
    [SerializeField]
    private ItemAction[] _actions;

    public ItemAction[] Actions => _actions;
}