using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory
{
    private readonly Player _player;
    private readonly List<InventoryItemStack> _stacks = new List<InventoryItemStack>();

    private bool Any(Item item) => _stacks.Any((InventoryItemStack i) => i.Item == item);

    private InventoryItemStack Find(Item item) => _stacks.Find((InventoryItemStack i) => i.Item == item);       

    public PlayerInventory(Player player) {
        _player = player;        
    }

    public bool Contains(Item item, int amount) => Find(item)?.Amount >= amount;

    public void Remove(Item item, int amount, bool send) {
        Debug.Assert(amount >= 1, "Amount must be greater than 1.");
        Debug.Assert(!(item.Property != ItemProperty.Stackable && amount != 1), "Can't remove more than 1 item if it's not stackable.");

        InventoryItemStack stack = Find(item);

        if (stack?.Amount >= amount) {
            stack.Remove(amount);
            if (stack.Amount <= 0)
                _stacks.Remove(stack);
            if (send)
                _player.Send(new MessageInventoryRemove(new ItemStackData(item.name, amount)));
        }
    }

    public bool Add(Item item, int amount, bool send) {
        Debug.Assert(amount >= 1, "Amount must be greater than 1.");
        Debug.Assert(!(item.Property != ItemProperty.Stackable && amount != 1), "Can't add more than 1 item if it's not stackable.");

        // already has unique item?
        if (item.Property == ItemProperty.Unique && Any(item)) {
            if (send)
                _player.Send(new MessageError(MessageErrorType.UniqueItem));
            return false;
        }
        // is stackable and already has a stack?
        else if (item.Property == ItemProperty.Stackable && Any(item)) {
            Find(item).Add(amount);
            if (send)
                _player.Send(new MessageInventoryAdd(new ItemStackData(item.name, amount)));
            return true;
        }
        // => not stackable
        // inventory full?
        else if (_stacks.Count >= SharedConfig.Current.InventorySpace) {
            if (send)
                _player.Send(new MessageError(MessageErrorType.InventoryFull));
            return false;
        }
        // doesn't have any?
        else {
            _stacks.Add(new InventoryItemStack(item, amount));
            if (send)
                _player.Send(new MessageInventoryAdd(new ItemStackData(item.name, amount)));
            return true;
        }
    }

}