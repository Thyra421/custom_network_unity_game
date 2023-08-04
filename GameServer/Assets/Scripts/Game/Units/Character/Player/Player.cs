using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAbilities))]
[RequireComponent(typeof(PlayerActivity))]
[RequireComponent(typeof(PlayerCooldowns))]
public class Player : Character
{
    [SerializeField]
    private PlayerMovement _movement;
    [SerializeField]
    private PlayerAbilities _abilities;
    [SerializeField]
    private PlayerActivity _activity;
    [SerializeField]
    private PlayerCooldowns _cooldowns;
    private PlayerHealth _health;

    public Client Client { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    public PlayerExperience Experience { get; private set; }
    public PlayerAnimation Animation { get; private set; }
    public PlayerAbilities Abilities => _abilities;
    public PlayerMovement Movement => _movement;
    public PlayerActivity Activity => _activity;
    public PlayerCooldowns Cooldowns => _cooldowns;
    public PlayerData Data => new PlayerData(Id, TransformData);
    public override CharacterHealth Health => _health;
    public override CharacterData CharacterData => new CharacterData(Id, CharacterType.Player);

    private void OnMessageMovement(MessageMovement messageMovement) {
        // has control and can move?
        if (!(HasControl && Movement.CanMove))
            return;

        // is moving while busy?
        if (Activity.IsBusy && TransformData.position.Distance(messageMovement.newTransform.position) > .1f)
            // cancel activity
            Activity.Stop();
        Movement.SetMovement(messageMovement.newTransform);
        Animation.SetAnimation(messageMovement.animation);
    }

    private void OnMessageUseAbility(MessageUseAbility messageUseAbility) {
        // has control?
        if (!HasControl) {
            Send(new MessageError(MessageErrorType.CantWhileStunned));
            return;
        }

        Ability ability = Resources.Load<Ability>($"{SharedConfig.Current.AbilitiesPath}/{messageUseAbility.abilityName}");

        // ability exists?
        if (ability == null) {
            Send(new MessageError(MessageErrorType.AbilityNotFound));
            return;
        }
        // in cooldown?
        if (Cooldowns.Any(ability)) {
            Send(new MessageError(MessageErrorType.InCooldown));
            return;
        }
        Abilities.UseAbility(ability, messageUseAbility.aimTarget.ToVector3);
    }

    private void OnMessageEquip(MessageEquip messageEquip) {
        // has control?
        if (!HasControl) {
            Send(new MessageError(MessageErrorType.CantWhileStunned));
            return;
        }

        Weapon weapon = Resources.Load<Weapon>($"{SharedConfig.Current.ItemsPath}/{messageEquip.weaponName}");

        // weapon exists?
        if (weapon == null) {
            Send(new MessageError(MessageErrorType.ObjectNotFound));
            return;
        }
        // has weapon in inventory?
        if (!Inventory.Contains(weapon, 1)) {
            Send(new MessageError(MessageErrorType.ObjectNotFound));
            return;
        }
        Abilities.Equip(weapon);
    }

    private void OnMessagePickUp(MessagePickUp messagePickUp) {
        // has control?
        if (!HasControl) {
            Send(new MessageError(MessageErrorType.CantWhileStunned));
            return;
        }

        Node node = Room.NodesManager.FindNode(messagePickUp.id);

        // node exists and has loots remaining?
        if (node == null || node.RemainingLoots <= 0) {
            Send(new MessageError(MessageErrorType.ObjectNotFound));
            return;
        }

        // player close enough?
        if (!node.GetComponentInChildren<Collider>().bounds.Intersects(GetComponent<Collider>().bounds)) {
            Send(new MessageError(MessageErrorType.TooFarAway));
            return;
        }

        // start channel
        Activity.Channel(() => {
            Item item = node.Loot;
            // can add to inventory?
            if (Inventory.Add(item, 1, true)) {
                node.RemoveOne();
                // node depleted?
                if (node.RemainingLoots <= 0)
                    Room.NodesManager.RemoveNode(node);
                // gain experience
                Experience.AddExperience(new ExperienceType[] { ExperienceType.General, node.DropSource.ExperienceType }, 5);
            }
            // else cancel channeling
            else
                Activity.Stop();
        }, "Picking up", node.RemainingLoots, .5f);
    }

    private void OnMessageCraft(MessageCraft messageCraft) {
        // has control?
        if (!HasControl) {
            Send(new MessageError(MessageErrorType.CantWhileStunned));
            return;
        }

        CraftingPattern pattern = Resources.Load<CraftingPattern>($"{SharedConfig.Current.CraftingPattersPath}/{messageCraft.directoryName}/{messageCraft.patternName}");

        // pattern exists?
        if (pattern == null) {
            Send(new MessageError(MessageErrorType.ObjectNotFound));
            return;
        }

        // has all reagents?
        foreach (ItemStack itemStack in pattern.Reagents)
            if (!Inventory.Contains(itemStack.Item, itemStack.Amount)) {
                Send(new MessageError(MessageErrorType.NotEnoughResources));
                return;
            }

        //start casting
        Activity.Cast(() => {
            // remove reagents from inventory
            foreach (ItemStack itemStack in pattern.Reagents)
                Inventory.Remove(itemStack.Item, itemStack.Amount, false);
            // has enough space?
            if (Inventory.Add(pattern.Outcome.Item, pattern.Outcome.Amount, false)) {
                Send(new MessageCrafted(pattern.Reagents.Select((ItemStack stack) => new ItemStackData(stack.Item.name, stack.Amount)).ToArray(), new ItemStackData(pattern.Outcome.Item.name, pattern.Outcome.Amount)));
                // gain experience
                Experience.AddExperience(new ExperienceType[] { ExperienceType.General, pattern.ExperienceType }, 20);
            }
            // else put the reagents back in the inventory
            else {
                foreach (ItemStack itemStack in pattern.Reagents)
                    Inventory.Add(itemStack.Item, itemStack.Amount, false);
                Send(new MessageError(MessageErrorType.NotEnoughInventorySpace));
            }
        }, "Crafting", 1);
    }

    private void OnMessageUseItem(MessageUseItem messageUseItem) {
        // has control?
        if (!HasControl) {
            Send(new MessageError(MessageErrorType.CantWhileStunned));
            return;
        }

        UsableItem item = Resources.Load<UsableItem>($"{SharedConfig.Current.ItemsPath}/{messageUseItem.itemName}");

        // item exists?
        if (item == null) {
            Send(new MessageError(MessageErrorType.ObjectNotFound));
            return;
        }
        // has item in inventory?
        if (!Inventory.Contains(item, 1)) {
            Send(new MessageError(MessageErrorType.ObjectNotFound));
            return;
        }
        // is in cooldown?
        if (Cooldowns.Any(item)) {
            Send(new MessageError(MessageErrorType.InCooldown));
            return;
        }

        DirectEffectController.Use(item, this);
    }

    protected override void Awake() {
        base.Awake();
        Inventory = new PlayerInventory(this);
        Experience = new PlayerExperience(this);
        Animation = new PlayerAnimation();
        _health = new PlayerHealth(this);
    }

    public void Initialize(Room room, Client client) {
        Initialize(room);
        Client = client;

        Client.UDP.MessageHandler.AddListener<MessageMovement>(OnMessageMovement);
        Client.TCP.MessageHandler.AddListener<MessageUseAbility>(OnMessageUseAbility);
        Client.TCP.MessageHandler.AddListener<MessageEquip>(OnMessageEquip);
        Client.TCP.MessageHandler.AddListener<MessagePickUp>(OnMessagePickUp);
        Client.TCP.MessageHandler.AddListener<MessageUseItem>(OnMessageUseItem);
        Client.TCP.MessageHandler.AddListener<MessageCraft>(OnMessageCraft);
    }

    /// <summary>
    /// Shortcut for Player.Client.TCP.Send(message).
    /// </summary>
    public void Send<T>(T message) {
        Client.TCP.Send(message);
    }
}
