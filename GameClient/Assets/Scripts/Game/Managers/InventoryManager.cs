using System;
using System.Linq;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public InventorySlot[] Slots { get; private set; }

    public delegate void OnChangedHandler(Item item, int amount);
    public event OnChangedHandler OnChanged;

    private InventorySlot Find(Item item) => Array.Find(Slots, (InventorySlot i) => i.Item == item);

    private bool Any(Item item) => Slots.Any((InventorySlot i) => i.Item == item);

    private InventorySlot EmptySlot => Array.Find(Slots, (InventorySlot i) => i.Item == null);

    private void Add(Item item, int amount) {
        InventorySlot slot;

        if (!Any(item) || !(item.Property == ItemProperty.Stackable)) {
            slot = EmptySlot;
            slot.Set(item, amount);
        } else {
            slot = Find(item);
            slot.Add(amount);
        }
        OnChanged?.Invoke(item, slot.Amount);
    }

    private void Remove(Item item, int amount) {
        InventorySlot slot = Find(item);
        slot.Remove(amount);
        OnChanged?.Invoke(item, slot.Amount);
    }

    private void OnMessageInventoryAdd(MessageInventoryAdd messageInventoryAdd) {
        Item item = Resources.Load<Item>($"{SharedConfig.Current.ItemsPath}/{messageInventoryAdd.data.itemName}");
        Add(item, messageInventoryAdd.data.amount);
    }

    private void OnMessageInventoryRemove(MessageInventoryRemove messageInventoryRemove) {
        Item item = Resources.Load<Item>($"{SharedConfig.Current.ItemsPath}/{messageInventoryRemove.data.itemName}");
        Remove(item, messageInventoryRemove.data.amount);
    }

    private void OnMessageCrafted(MessageCrafted messageCrafted) {
        foreach (ItemStackData r in messageCrafted.reagents) {
            Item reagent = Resources.Load<Item>($"{SharedConfig.Current.ItemsPath}/{r.itemName}");
            Remove(reagent, r.amount);
        }
        Item outcome = Resources.Load<Item>($"{SharedConfig.Current.ItemsPath}/{messageCrafted.outcome.itemName}");
        Add(outcome, messageCrafted.outcome.amount);
    }

    protected override void Awake() {
        base.Awake();

        Slots = new InventorySlot[SharedConfig.Current.InventorySpace];
        for (int i = 0; i < Slots.Length; i++)
            Slots[i] = new InventorySlot();


        TCPClient.MessageRegistry.AddListener<MessageInventoryAdd>(OnMessageInventoryAdd);
        TCPClient.MessageRegistry.AddListener<MessageInventoryRemove>(OnMessageInventoryRemove);
        TCPClient.MessageRegistry.AddListener<MessageCrafted>(OnMessageCrafted);
    }

    public void ADD_TEST_ITEM(Item i) {
        Add(i, 1);
    }
}