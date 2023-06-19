using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TCPClient
{
    private NetworkStream _stream;
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
        }
    }

    private void OnMessageAuthenticate(MessageAuthenticate clientMessageAuthenticate) {
        _client.Authenticate(new UDPClient(clientMessageAuthenticate.udpAddress, clientMessageAuthenticate.udpPort), clientMessageAuthenticate.secret);
    }

    private async void OnMessagePlay(MessagePlay clientMessagePlay) {
        Player newPlayer = Reception.Current.JoinOrCreateRoom(_client);
        newPlayer.Room.BroadcastTCP(new MessageJoinedGame(newPlayer.Data), newPlayer);
        MessageGameState messageGameState = new MessageGameState(newPlayer.Id, newPlayer.Room.PlayerDatas, newPlayer.Room.ObjectDatas);
        Debug.Log("send");
        await Send(messageGameState);
    }

    private void OnMessageAttack(MessageAttack clientMessageAttack) {
        _client.Player.Room.BroadcastTCP(new MessageAttacked(_client.Player.Id), _client.Player);
        _client.Player.Attack();
    }

    private void OnMessagePickUp(MessagePickUp clientMessagePickUp) {
        Mushroom mushroom = _client.Player.Room.FindMushroom(clientMessagePickUp.id);
        Player player = _client.Player;

        if (mushroom.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds)) {
            _client.Player.Room.BroadcastTCP(new MessagePickedUp(_client.Player.Id, clientMessagePickUp.id));
            player.Room.RemoveMushroom(mushroom);
        }
    }

    public TCPClient(TcpClient tcpClient) {
        _stream = tcpClient.GetStream();
        Listen();
    }

    public async Task Send<T>(T message) {
        byte[] msg = Encoding.ASCII.GetBytes(Utils.Serialize(message));
        await _stream.WriteAsync(msg, 0, msg.Length);
    }

    public async void Listen() {
        byte[] bytes = new byte[2048];
        int i;

        try {
            while ((i = await _stream.ReadAsync(bytes, 0, bytes.Length)) != 0) {
                string message = Encoding.ASCII.GetString(bytes, 0, i);
                OnMessage(message);
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
