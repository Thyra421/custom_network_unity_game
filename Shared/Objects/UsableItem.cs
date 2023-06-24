using System;
using System.Linq;
using UnityEngine;

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

    public void Use(Player player) {
        ItemActionController itemActionController = new ItemActionController(player);

        foreach (ItemAction entry in _actions) {
            typeof(ItemActionController).GetMethod(entry.MethodName).Invoke(itemActionController, entry.Parameters.Select((ActionParameter param) => param.ToObject).ToArray());
        }
    }
}