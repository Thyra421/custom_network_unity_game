using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TCPClient
{
    private readonly NetworkStream _stream;

    private Player Player => Client.Player;
    private Room Room => Player.Room;

    public Client Client { get; set; }

    private void OnDisconnect() {
        Debug.Log($"[TCPClient] disconnected");

        if (Player)
            Room.PlayersManager.RemovePlayer(Player);

        API.Clients.Remove(Client);
    }

    private void OnMessage(string message) {
        Debug.Log($"[TCPClient] received {message}");

        Type messageType = Utils.GetMessageType(message);

        if (messageType.Equals(typeof(MessageAuthenticate))) {
            MessageAuthenticate messageAuthenticate = Utils.Deserialize<MessageAuthenticate>(message);
            OnMessageAuthenticate(messageAuthenticate);
        } else if (messageType.Equals(typeof(MessagePlay))) {
            MessagePlay messagePlay = Utils.Deserialize<MessagePlay>(message);
            OnMessagePlay(messagePlay);
        } else if (messageType.Equals(typeof(MessageUseAbility))) {
            MessageUseAbility messageUseAbility = Utils.Deserialize<MessageUseAbility>(message);
            OnMessageUseAbility(messageUseAbility);
        } else if (messageType.Equals(typeof(MessagePickUp))) {
            MessagePickUp messagePickUp = Utils.Deserialize<MessagePickUp>(message);
            OnMessagePickUp(messagePickUp);
        } else if (messageType.Equals(typeof(MessageCraft))) {
            MessageCraft messageCraft = Utils.Deserialize<MessageCraft>(message);
            OnMessageCraft(messageCraft);
        } else if (messageType.Equals(typeof(MessageUseItem))) {
            MessageUseItem messageUseItem = Utils.Deserialize<MessageUseItem>(message);
            OnMessageUseItem(messageUseItem);
        } else if (messageType.Equals(typeof(MessageEquip))) {
            MessageEquip messageEquip = Utils.Deserialize<MessageEquip>(message);
            OnMessageEquip(messageEquip);
        }
    }

    private void OnMessageAuthenticate(MessageAuthenticate messageAuthenticate) {
        Client.Authenticate(new UDPClient(messageAuthenticate.udpAddress, messageAuthenticate.udpPort), messageAuthenticate.secret);
    }

    private void OnMessagePlay(MessagePlay messagePlay) {
        Reception.Current.JoinOrCreateRoom(Client);
    }

    private void OnMessageUseAbility(MessageUseAbility messageUseAbility) {
        if (!Player.HasControl) {
            Send(new MessageError(MessageErrorType.CantWhileStunned));
            return;
        }

        Ability ability = Resources.Load<Ability>($"{SharedConfig.Current.AbilitiesPath}/{messageUseAbility.abilityName}");

        // ability exists?
        if (ability == null) {
            Send(new MessageError(MessageErrorType.AbilityNotFound));
            return;
        }
        Player.Abilities.UseAbility(ability, messageUseAbility.aimTarget.ToVector3);
    }

    private void OnMessageEquip(MessageEquip messageEquip) {
        if (!Player.HasControl) {
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
        if (!Player.Inventory.Contains(weapon, 1)) {
            Send(new MessageError(MessageErrorType.ObjectNotFound));
            return;
        }
        Player.Abilities.Equip(weapon);
    }

    private void OnMessagePickUp(MessagePickUp messagePickUp) {
        if (!Player.HasControl) {
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
        if (!node.GetComponentInChildren<Collider>().bounds.Intersects(Player.GetComponent<Collider>().bounds)) {
            Send(new MessageError(MessageErrorType.TooFarAway));
            return;
        }
        // start channel
        Player.Activity.Channel(() => {
            Item item = node.Loot;
            // can add to inventory?
            if (Player.Inventory.Add(item, 1, true)) {
                node.RemoveOne();
                // node depleted?
                if (node.RemainingLoots <= 0)
                    Room.NodesManager.RemoveNode(node);
                // gain experience
                Player.Experience.AddExperience(new ExperienceType[] { ExperienceType.General, node.DropSource.ExperienceType }, 5);
            }
            // else cancel channeling
            else
                Player.Activity.Stop();
        }, "Picking up", node.RemainingLoots, .5f);
    }

    private void OnMessageCraft(MessageCraft messageCraft) {
        if (!Player.HasControl) {
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
            if (!Player.Inventory.Contains(itemStack.Item, itemStack.Amount)) {
                Send(new MessageError(MessageErrorType.NotEnoughResources));
                return;
            }
        //start casting
        Player.Activity.Cast(() => {
            // remove reagents from inventory
            foreach (ItemStack itemStack in pattern.Reagents)
                Player.Inventory.Remove(itemStack.Item, itemStack.Amount, false);
            // has enough space?
            if (Player.Inventory.Add(pattern.Outcome.Item, pattern.Outcome.Amount, false)) {
                Send(new MessageCrafted(pattern.Reagents.Select((ItemStack stack) => new ItemStackData(stack.Item.name, stack.Amount)).ToArray(), new ItemStackData(pattern.Outcome.Item.name, pattern.Outcome.Amount)));
                // gain experience
                Player.Experience.AddExperience(new ExperienceType[] { ExperienceType.General, pattern.ExperienceType }, 20);
            }
            // else put the reagents back in the inventory
            else {
                foreach (ItemStack itemStack in pattern.Reagents)
                    Player.Inventory.Add(itemStack.Item, itemStack.Amount, false);
                Send(new MessageError(MessageErrorType.NotEnoughInventorySpace));
            }
        }, "Crafting", 1);
    }

    private void OnMessageUseItem(MessageUseItem messageUseItem) {
        if (!Player.HasControl) {
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
        if (!Player.Inventory.Contains(item, 1)) {
            Send(new MessageError(MessageErrorType.ObjectNotFound));
            return;
        }
        // is in cooldown?
        if (Player.Cooldowns.Any(item)) {
            Send(new MessageError(MessageErrorType.InCooldown));
            return;
        }

        Player.DirectEffectController.Use(item, Player);
    }

    public TCPClient(TcpClient tcpClient) {
        _stream = tcpClient.GetStream();
        Listen();
    }

    public void Send<T>(T message) {
        string serializedMessage = Utils.Serialize(message);
        serializedMessage += '#';
        byte[] bytes = Encoding.ASCII.GetBytes(serializedMessage);

        int batchSize = SharedConfig.Current.TCPBatchSize;
        int i = 0;

        while (i < bytes.Length) {
            if (i + batchSize >= bytes.Length) {
                _stream.Write(bytes, i, bytes.Length - i);
                i = bytes.Length;
            } else {
                _stream.Write(bytes, i, batchSize);
                i += batchSize;
            }
        }
    }

    public async void Listen() {
        byte[] bytes = new byte[SharedConfig.Current.TCPBatchSize];
        int i;
        string buffer = "";

        try {
            while ((i = await _stream.ReadAsync(bytes, 0, bytes.Length)) != 0) {
                string message = Encoding.ASCII.GetString(bytes, 0, i);
                while (message.Contains('#')) {
                    int endIndex = message.IndexOf('#');
                    buffer += message[..endIndex];
                    OnMessage(buffer);
                    buffer = "";
                    if (endIndex < message.Length)
                        message = message[(endIndex + 1)..];
                }
                buffer += message;
            }
            OnDisconnect();
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
