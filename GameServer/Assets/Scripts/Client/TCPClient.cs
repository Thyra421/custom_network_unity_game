using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
            MessageAuthenticate clientMessageAuthenticate = Utils.Deserialize<MessageAuthenticate>(message);
            OnMessageAuthenticate(clientMessageAuthenticate);
        } else if (messageType.Equals(typeof(MessagePlay))) {
            MessagePlay clientMessagePlay = Utils.Deserialize<MessagePlay>(message);
            OnMessagePlay(clientMessagePlay);
        } else if (messageType.Equals(typeof(MessageAttack))) {
            MessageAttack clientMessageAttack = Utils.Deserialize<MessageAttack>(message);
            OnMessageAttack(clientMessageAttack);
        } else if (messageType.Equals(typeof(MessagePickUp))) {
            MessagePickUp clientMessagePickUp = Utils.Deserialize<MessagePickUp>(message);
            OnMessagePickUp(clientMessagePickUp);
        } else if (messageType.Equals(typeof(MessageCraft))) {
            MessageCraft clientMessageCraft = Utils.Deserialize<MessageCraft>(message);
            OnMessageCraft(clientMessageCraft);
        } else if (messageType.Equals(typeof(MessageUseItem))) {
            MessageUseItem clientMessageUseItem = Utils.Deserialize<MessageUseItem>(message);
            OnMessageUseItem(clientMessageUseItem);
        }
    }

    private void OnMessageAuthenticate(MessageAuthenticate clientMessageAuthenticate) {
        _client.Authenticate(new UDPClient(clientMessageAuthenticate.udpAddress, clientMessageAuthenticate.udpPort), clientMessageAuthenticate.secret);
    }

    private void OnMessagePlay(MessagePlay clientMessagePlay) {
        Player newPlayer = Reception.Current.JoinOrCreateRoom(_client);
        newPlayer.Room.PlayersManager.BroadcastTCP(new MessageJoinedGame(newPlayer.Data), newPlayer);

        MessageGameState messageGameState = new MessageGameState(newPlayer.Id, newPlayer.Room.PlayersManager.PlayerDatas);
        MessageSpawnNodes messageSpawnNodes = new MessageSpawnNodes(newPlayer.Room.NodesManager.NodeDatas);
        Send(messageGameState);
        Send(messageSpawnNodes);
    }

    private void OnMessageAttack(MessageAttack clientMessageAttack) {
        _client.Player.Room.PlayersManager.BroadcastTCP(new MessageAttacked(_client.Player.Id), _client.Player);
        _client.Player.Attack.Attack();
    }

    private void OnMessagePickUp(MessagePickUp clientMessagePickUp) {
        Player player = _client.Player;
        Node node = player.Room.NodesManager.FindNode(clientMessagePickUp.id);

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

    private void OnMessageUseItem(MessageUseItem clientMessageUseItem) {
        UsableItem item = Resources.Load<UsableItem>($"{SharedConfig.CRAFTED_ITEMS_PATH}/{clientMessageUseItem.itemName}");
        _client.Player.ItemActionController.Use(item);
    }

    public TCPClient(TcpClient tcpClient) {
        _stream = tcpClient.GetStream();
        Listen();
    }

    public async void Send<T>(T message) {
        string serializedMessage = Utils.Serialize(message);
        serializedMessage += '#';
        byte[] bytes = Encoding.ASCII.GetBytes(serializedMessage);

        int batchSize = SharedConfig.TCP_BATCH_SIZE;
        int i = 0;

        while (i < bytes.Length) {
            if (i + batchSize >= bytes.Length) {
                await _stream.WriteAsync(bytes, i, bytes.Length - i);
                i = bytes.Length;
            } else {
                await _stream.WriteAsync(bytes, i, batchSize);
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
