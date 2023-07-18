﻿using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TCPClient
{
    private readonly NetworkStream _stream;
    private Client _client;

    private void OnDisconnect() {
        Debug.Log($"[TCPClient] disconnected");

        Player player = _client.Player;
        if (player != null) {
            player.Room.PlayersManager.RemovePlayer(player);
            player.Room.PlayersManager.BroadcastTCP(new MessageLeftGame(player.Id), player);
        }
        API.Clients.Remove(_client);
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
        _client.Authenticate(new UDPClient(messageAuthenticate.udpAddress, messageAuthenticate.udpPort), messageAuthenticate.secret);
    }

    private void OnMessagePlay(MessagePlay messagePlay) {
        Player newPlayer = Reception.Current.JoinOrCreateRoom(_client);
        newPlayer.Room.PlayersManager.BroadcastTCP(new MessageJoinedGame(newPlayer.Data), newPlayer);

        MessageGameState messageGameState = new MessageGameState(newPlayer.Id, newPlayer.Room.PlayersManager.PlayerDatas);
        MessageSpawnNodes messageSpawnNodes = new MessageSpawnNodes(newPlayer.Room.NodesManager.NodeDatas);
        MessageSpawnNPCs messageSpawnNPCs = new MessageSpawnNPCs(newPlayer.Room.NPCsManager.NPCDatas);
        Send(messageGameState);
        Send(messageSpawnNodes);
        Send(messageSpawnNPCs);
    }

    private void OnMessageUseAbility(MessageUseAbility messageUseAbility) {
        Ability ability = Resources.Load<Ability>($"{SharedConfig.ABILITIES_PATH}/{messageUseAbility.abilityName}");
        if (ability != null)
            _client.Player.Abilities.UseAbility(ability);
        else
            Send(new MessageError(MessageErrorType.abilityNotFound));
    }

    private void OnMessageEquip(MessageEquip messageEquip) {
        Weapon weapon = Resources.Load<Weapon>($"{SharedConfig.ITEMS_PATH}/{messageEquip.weaponName}");
        if (weapon != null)
            _client.Player.Abilities.Equip(weapon);
        else
            Send(new MessageError(MessageErrorType.objectNotFound));
    }

    private void OnMessagePickUp(MessagePickUp messagePickUp) {
        Player player = _client.Player;
        Node node = player.Room.NodesManager.FindNode(messagePickUp.id);

        // node doesn't exist?
        if (node == null || node.RemainingLoots <= 0) {
            Send(new MessageError(MessageErrorType.objectNotFound));
            return;
        }
        // player too far away?
        if (!node.GetComponentInChildren<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds)) {
            Send(new MessageError(MessageErrorType.tooFarAway));
            return;
        }
        // start channel
        player.Activity.Channel(() => {
            Item item = node.Loot;
            // can add to inventory?
            if (player.Inventory.Add(item, 1, true)) {
                node.RemoveOne();
                // node depleted?
                if (node.RemainingLoots <= 0)
                    player.Room.NodesManager.RemoveNode(node);
                // gain experience
                PlayerSkillExperience skillExperience = player.Experience.GetSkillExperience(node.DropSource.SkillType);
                skillExperience.AddExperience(5);
            }
            // else cancel channeling
            else
                player.Activity.Stop();
        }, "Picking up", node.RemainingLoots, .5f);
    }

    private void OnMessageCraft(MessageCraft messageCraft) {
        CraftingPattern pattern = Resources.Load<CraftingPattern>($"{SharedConfig.CRAFTING_PATTERNS_PATH}/{messageCraft.directoryName}/{messageCraft.patternName}");

        // pattern exists?
        if (pattern == null) {
            Send(new MessageError(MessageErrorType.objectNotFound));
            return;
        }
        // has all reagents?
        foreach (ItemStack itemStack in pattern.Reagents)
            if (!_client.Player.Inventory.Contains(itemStack.Item, itemStack.Amount)) {
                Send(new MessageError(MessageErrorType.notEnoughResources));
                return;
            }
        //start casting
        _client.Player.Activity.Cast(() => {
            // remove reagents from inventory
            foreach (ItemStack itemStack in pattern.Reagents)
                _client.Player.Inventory.Remove(itemStack.Item, itemStack.Amount, false);
            // has enough space?
            if (_client.Player.Inventory.Add(pattern.Outcome.Item, pattern.Outcome.Amount, false)) {
                Send(new MessageCrafted(pattern.Reagents.Select((ItemStack stack) => new ItemStackData(stack.Item.name, stack.Amount)).ToArray(), new ItemStackData(pattern.Outcome.Item.name, pattern.Outcome.Amount)));
            }
            // else put the reagents back in the inventory
            else {
                foreach (ItemStack itemStack in pattern.Reagents)
                    _client.Player.Inventory.Add(itemStack.Item, itemStack.Amount, false);
                Send(new MessageError(MessageErrorType.notEnoughInventorySpace));
            }
        }, "Crafting", 1);
    }

    private void OnMessageUseItem(MessageUseItem messageUseItem) {
        UsableItem item = Resources.Load<UsableItem>($"{SharedConfig.ITEMS_PATH}/{messageUseItem.itemName}");
        if (item != null)
            _client.Player.ItemEffectController.Use(item);
    }

    public TCPClient(TcpClient tcpClient) {
        _stream = tcpClient.GetStream();
        Listen();
    }

    public void Send<T>(T message) {
        string serializedMessage = Utils.Serialize(message);
        serializedMessage += '#';
        byte[] bytes = Encoding.ASCII.GetBytes(serializedMessage);

        int batchSize = SharedConfig.TCP_BATCH_SIZE;
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
        byte[] bytes = new byte[SharedConfig.TCP_BATCH_SIZE];
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

    public Client Client {
        get => _client;
        set => _client = value;
    }
}
