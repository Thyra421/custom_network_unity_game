using System;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _current;
    private readonly InventorySlot[] _slots = new InventorySlot[SharedConfig.INVENTORY_SPACE];
    private event OnChangedHandler _onChanged;

    private InventorySlot Find(Item item) => Array.Find(_slots, (InventorySlot i) => i.Item == item);

    private bool Any(Item item) => _slots.Any((InventorySlot i) => i.Item == item);

    private InventorySlot EmptySlot => Array.Find(_slots, (InventorySlot i) => i.Item == null);

    private void Add(Item item, int amount) {
        InventorySlot slot;

        if (!Any(item) || !(item.Property == ItemProperty.Stackable)) {
            slot = EmptySlot;
            slot.Set(item, amount);
        } else {
            slot = Find(item);
            slot.Add(amount);
        }
        _onChanged?.Invoke(item, slot.Amount);
    }

    private void Remove(Item item, int amount) {
        InventorySlot slot = Find(item);
        slot.Remove(amount);
        _onChanged?.Invoke(item, slot.Amount);
    }

    private void OnMessageInventoryAdd(MessageInventoryAdd messageInventoryAdd) {
        Item item = Resources.Load<Item>($"{SharedConfig.ITEMS_PATH}/{messageInventoryAdd.data.itemName}");
        Add(item, messageInventoryAdd.data.amount);
    }

    private void OnMessageInventoryRemove(MessageInventoryRemove messageInventoryRemove) {
        Item item = Resources.Load<Item>($"{SharedConfig.ITEMS_PATH}/{messageInventoryRemove.data.itemName}");
        Remove(item, messageInventoryRemove.data.amount);
    }

    private void OnMessageCrafted(MessageCrafted messageCrafted) {
        foreach (ItemStackData r in messageCrafted.reagents) {
            Item reagent = Resources.Load<Item>($"{SharedConfig.ITEMS_PATH}/{r.itemName}");
            Remove(reagent, r.amount);
        }
        Item outcome = Resources.Load<Item>($"{SharedConfig.ITEMS_PATH}/{messageCrafted.outcome.itemName}");
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

    public delegate void OnChangedHandler(Item item, int amount);

    public event OnChangedHandler OnChanged {
        add => _onChanged += value;
        remove => _onChanged -= value;
    }

    public void ADD_TEST_ITEM(Item i) {
        Add(i, 1);
    }

    public InventorySlot[] Slots => _slots;

    public static InventoryManager Current => _current;
}