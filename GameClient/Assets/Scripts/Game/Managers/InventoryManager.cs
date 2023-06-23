using System.Linq;
using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private readonly InventorySlot[] _slots = new InventorySlot[SharedConfig.INVENTORY_SPACE];

    private InventorySlot Find(Item item) => Array.Find(_slots, (InventorySlot i) => i.Item == item);

    private bool Any(Item item) => _slots.Any((InventorySlot i) => i.Item == item);

    private InventorySlot EmptySlot => Array.Find(_slots, (InventorySlot i) => i.Item == null);

    private void Add(Item item, int amount) {
        if (!Any(item) || !(item.Property == ItemProperty.Stackable))
            EmptySlot.Set(item, amount);
        else
            Find(item).Add(amount);
    }

    private void Remove(Item item, int amount) {
        Find(item).Remove(amount);
    }

    private void OnMessageInventoryAdd(MessageInventoryAdd messageInventoryAdd) {
        Item item = Resources.Load<Item>("Shared/RawMaterials/" + messageInventoryAdd.itemName);
        Add(item, messageInventoryAdd.amount);
    }

    private void OnMessageInventoryRemove(MessageInventoryRemove messageInventoryRemove) {
        Item item = Resources.Load<Item>("Shared/RawMaterials/" + messageInventoryRemove.itemName);
        Remove(item, messageInventoryRemove.amount);
    }

    private void Awake() {
        for (int i = 0; i < _slots.Length; i++)
            _slots[i] = new InventorySlot();
        MessageHandler.OnMessageInventoryAddEvent += OnMessageInventoryAdd;
        MessageHandler.OnMessageInventoryRemoveEvent += OnMessageInventoryRemove;
    }

    public InventorySlot[] Slots => _slots;
}