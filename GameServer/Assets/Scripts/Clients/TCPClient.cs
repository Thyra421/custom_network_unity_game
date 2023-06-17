using System;
using System.Linq;
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

        Player player = API.Players.Find(this);
        if (player != null) {
            API.Players.Remove(player);
            API.Players.BroadcastTCP(new ServerMessageLeftGame(player.Data.id), player);
        }
        API.Clients.Remove(this);
    }

    private void OnMessage(string message) {
        Debug.Log($"[TCPClient] received {message}");

        ClientMessage clientMessage = Utils.ParseJsonString<ClientMessage>(message);

        switch (clientMessage.type) {
            case ClientMessageType.authenticate:
                ClientMessageAuthenticate clientMessageAuthenticate = Utils.ParseJsonString<ClientMessageAuthenticate>(message);
                OnMessageAuthenticate(clientMessageAuthenticate);
                break;
            case ClientMessageType.play:
                ClientMessagePlay clientMessagePlay = Utils.ParseJsonString<ClientMessagePlay>(message);
                OnMessagePlay(clientMessagePlay);
                break;
        }
    }

    private void OnMessageAuthenticate(ClientMessageAuthenticate clientMessageAuthenticate) {
        _client?.Authenticate(new UDPClient(clientMessageAuthenticate.udpAddress, clientMessageAuthenticate.udpPort), clientMessageAuthenticate.secret);
    }

    private async void OnMessagePlay(ClientMessagePlay clientMessagePlay) {
        Player newPlayer = API.Players.Create(_client);
        API.Players.BroadcastTCP(new ServerMessageJoinedGame(newPlayer.Data), newPlayer);
        ServerMessageGameState messageGameState = new ServerMessageGameState(newPlayer.Data.id, API.Players.GetObjectDatas().ToArray());
        await Send(messageGameState);
    }

    public TCPClient(TcpClient tcpClient) {
        _stream = tcpClient.GetStream();
        Listen();
    }

    public async Task Send(ServerMessage message) {
        byte[] msg = Encoding.ASCII.GetBytes(Utils.ObjectToString(message));
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
        } catch (Exception) {
            OnDisconnect();
        }
    }

    public Client Client {
        get => _client;
        set => _client = value;
    }
}
