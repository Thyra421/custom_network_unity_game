using System;
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
            player.Room.RemovePlayer(player);
            player.Room.BroadcastTCP(new MessageLeftGame(player.Id), player);
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
        }
    }

    private void OnMessageAuthenticate(MessageAuthenticate clientMessageAuthenticate) {
        _client.Authenticate(new UDPClient(clientMessageAuthenticate.udpAddress, clientMessageAuthenticate.udpPort), clientMessageAuthenticate.secret);
    }

    private async void OnMessagePlay(MessagePlay clientMessagePlay) {
        Player newPlayer = Reception.Current.JoinOrCreateRoom(_client);
        newPlayer.Room.BroadcastTCP(new MessageJoinedGame(newPlayer.Data), newPlayer);
        MessageGameState messageGameState = new MessageGameState(newPlayer.Id, newPlayer.Room.PlayerDatas);
        MessageSpawnNodes messageSpawnNodes = new MessageSpawnNodes(newPlayer.Room.NodeDatas);
        await Send(messageGameState);
        await Send(messageSpawnNodes);
    }

    private void OnMessageAttack(MessageAttack clientMessageAttack) {
        _client.Player.Room.BroadcastTCP(new MessageAttacked(_client.Player.Id), _client.Player);
        _client.Player.Attack();
    }

    private async void OnMessagePickUp(MessagePickUp clientMessagePickUp) {
        Node node = _client.Player.Room.FindNode(clientMessagePickUp.id);
        if (node == null || node.RemainingLoots <= 0) {
            await Send(new MessageError(MessageErrorType.objectNotFound));
            return;
        }
        Player player = _client.Player;

        if (node.GetComponentInChildren<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds)) {
            Item item = node.Loot;
            bool result = await _client.Player.Inventory.Add(item, 1, true);
            if (result) {
                node.RemoveOne();
                if (node.RemainingLoots <= 0) {
                    player.Room.RemoveNode(node);
                    _client.Player.Room.BroadcastTCP(new MessageDespawnObject(node.Id));
                } else
                    _client.Player.Room.BroadcastTCP(new MessageLooted(node.Id));
            }
        }
    }

    private async void OnMessageCraft(MessageCraft messageCraft) {
        CraftingPattern pattern = Resources.Load<CraftingPattern>($"{SharedConfig.CRAFTING_PATTERNS_PATH}/{messageCraft.patternName}");

        // pattern exists?
        if (pattern == null) {
            await Send(new MessageError(MessageErrorType.objectNotFound));
            return;
        }
        // has all reagents?
        foreach (ItemStack itemStack in pattern.Reagents)
            if (!_client.Player.Inventory.Contains(itemStack.Item, itemStack.Amount)) {
                await Send(new MessageError(MessageErrorType.notEnoughResources));
                return;
            }
        foreach (ItemStack itemStack in pattern.Reagents)
            _client.Player.Inventory.Remove(itemStack.Item, itemStack.Amount, false);
        // has enough space?
        if (await _client.Player.Inventory.Add(pattern.Outcome.Item, pattern.Outcome.Amount, false)) {
            await Send(new MessageCrafted(pattern.Reagents.Select((ItemStack stack) => new ItemStackData(stack.Item.name, stack.Amount)).ToArray(), new ItemStackData(pattern.Outcome.Item.name, pattern.Outcome.Amount)));
        }
        // put the reagents back in the inventory
        else {
            foreach (ItemStack itemStack in pattern.Reagents)
                await _client.Player.Inventory.Add(itemStack.Item, itemStack.Amount, false);
            await Send(new MessageError(MessageErrorType.notEnoughInventorySpace));
        }
    }

    public TCPClient(TcpClient tcpClient) {
        _stream = tcpClient.GetStream();
        Listen();
    }

    public async Task Send<T>(T message) {
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
