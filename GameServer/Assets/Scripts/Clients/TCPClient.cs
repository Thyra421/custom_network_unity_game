using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;

public class TCPClient
{
    private NetworkStream _stream;
    private Client _client;

    private void OnDisconnect() {
        Debug.Log($"[TCPClient] disconnected");

        Player player = API.Players.Find(this);
        if (player != null) {
            API.Players.Remove(player);
            API.Players.BroadcastTCP(new MessageLeftGame(player.Data.id), player);
        }
        API.Clients.Remove(this);
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
        }
    }

    private void OnMessageAuthenticate(MessageAuthenticate clientMessageAuthenticate) {
        _client?.Authenticate(new UDPClient(clientMessageAuthenticate.udpAddress, clientMessageAuthenticate.udpPort), clientMessageAuthenticate.secret);
    }

    private async void OnMessagePlay(MessagePlay clientMessagePlay) {
        Player newPlayer = API.Players.Create(_client);
        API.Players.BroadcastTCP(new MessageJoinedGame(newPlayer.Data), newPlayer);
        MessageGameState messageGameState = new MessageGameState(newPlayer.Data.id, API.Players.GetObjectDatas().ToArray());
        await Send(messageGameState);
    }

    private void OnMessageAttack(MessageAttack clientMessageAttack) {
        API.Players.BroadcastTCP(new MessagePlayerAttack(_client.Player.Data.id), _client.Player);
        _client.Player.Avatar.Attack();
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
            OnDisconnect();
        }
    }

    public Client Client {
        get => _client;
        set => _client = value;
    }
}
