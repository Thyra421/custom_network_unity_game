using Newtonsoft.Json.Linq;
using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TCPClient
{
    private static TcpClient _tcpClient;

    private static async void Listen() {
        if (_tcpClient == null || !_tcpClient.Connected) {
            OnError();
        }
        OnListen();
        NetworkStream stream = _tcpClient!.GetStream();

        byte[] bytes = new byte[2048];
        int i;

        try {
            while ((i = await stream.ReadAsync(bytes, 0, bytes.Length)) != 0) {
                string message = Encoding.ASCII.GetString(bytes, 0, i);
                OnMessage(message);
            }
            OnDisconnect();
        } catch (Exception e) {
            OnDisconnect();
        }
    }

    private static void OnError() {
        Debug.Log($"[TCP Client] not connected");
    }

    private static void OnListen() {
        Debug.Log($"[TCP Client] listening");
    }

    private static void OnDisconnect() {
        Debug.Log($"[TCP Client] client disconnected");
        SceneLoader.Current.LoadMenuAsync();
    }

    private static void OnConnectionFailed() {
        Debug.Log($"[TCP Client] connection failed");
    }

    private static void OnConnected() {
        Debug.Log($"[TCP Client] connected");
    }

    private static void OnMessage(string message) {
        Debug.Log($"[TCP Client] received {message}");

        ServerMessage serverMessage = Utils.ParseJsonString<ServerMessage>(message);

        switch (serverMessage.type) {
            case ServerMessageType.joinedGame:
                ServerMessageJoinedGame messageJoinedGame = Utils.ParseJsonString<ServerMessageJoinedGame>(message);
                MessageHandler.Current.onServerMessageJoinedGame(messageJoinedGame);
                break;
            case ServerMessageType.leftGame:
                ServerMessageLeftGame messageLeftGame = Utils.ParseJsonString<ServerMessageLeftGame>(message);
                MessageHandler.Current.onServerMessageLeftGame(messageLeftGame);
                break;
            case ServerMessageType.gameState:
                ServerMessageGameState messageGameState = Utils.ParseJsonString<ServerMessageGameState>(message);
                MessageHandler.Current.onServerMessageGameState(messageGameState);
                break;
        }
    }

    public static void Connect() {
        _tcpClient = new TcpClient(Config.ServerAddress, Config.ServerPortTCP);
        OnConnected();
        Listen();
    }

    public static async void Send(ClientMessage message) {
        if (_tcpClient == null || !_tcpClient.Connected) {
            OnError();
        } else {
            JObject json = JObject.FromObject(message);
            byte[] data = Encoding.ASCII.GetBytes(json.ToString());
            await _tcpClient!.GetStream().WriteAsync(data, 0, data.Length);
        }
    }
}
