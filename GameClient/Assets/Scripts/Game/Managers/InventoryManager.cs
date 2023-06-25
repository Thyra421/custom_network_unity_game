using System;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _current;
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
        Item item = Resources.Load<Item>($"{SharedConfig.RAW_MATERIALS_PATH}/{messageInventoryAdd.data.itemName}");
        Add(item, messageInventoryAdd.data.amount);
    }

    private void OnMessageInventoryRemove(MessageInventoryRemove messageInventoryRemove) {
        Item item = Resources.Load<Item>($"{SharedConfig.RAW_MATERIALS_PATH}/{messageInventoryRemove.data.itemName}");
        Remove(item, messageInventoryRemove.data.amount);
    }

    private void OnMessageCrafted(MessageCrafted messageCrafted) {
        foreach (ItemStackData r in messageCrafted.reagents) {
            Item reagent = Resources.Load<Item>($"{SharedConfig.RAW_MATERIALS_PATH}/{r.itemName}");
            Remove(reagent, r.amount);
        }
        Item outcome = Resources.Load<Item>($"{SharedConfig.CRAFTED_ITEMS_PATH}/{messageCrafted.outcome.itemName}");
        Add(outcome, messageCrafted.outcome.amount);
    }

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
        for (int i = 0; i < _slots.Length; i++)
            _slots[i] = new InventorySlot();
        MessageHandler.Current.OnMessageInventoryAddEvent += OnMessageInventoryAdd;
        MessageHandler.Current.OnMessageInventoryRemoveEvent += OnMessageInventoryRemove;
        MessageHandler.Current.OnMessageCraftedEvent += OnMessageCrafted;
    }

    public InventorySlot[] Slots => _slots;

    public static InventoryManager Current => _current;
}