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
            Debug.LogException(e);
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

        Type messageType = Utils.GetMessageType(message);

        if (messageType.Equals(typeof(MessageJoinedGame))) {
            MessageJoinedGame messageJoinedGame = Utils.Deserialize<MessageJoinedGame>(message);
            MessageHandler.Current.onMessageJoinedGame(messageJoinedGame);
        } else if (messageType.Equals(typeof(MessageLeftGame))) {
            MessageLeftGame messageLeftGame = Utils.Deserialize<MessageLeftGame>(message);
            MessageHandler.Current.onMessageLeftGame(messageLeftGame);
        } else if (messageType.Equals(typeof(MessageGameState))) {
            MessageGameState messageGameState = Utils.Deserialize<MessageGameState>(message);
            MessageHandler.Current.onMessageGameState(messageGameState);
        } else if (messageType.Equals(typeof(MessageAttacked))) {
            MessageAttacked messageAttacked = Utils.Deserialize<MessageAttacked>(message);
            MessageHandler.Current.onMessageAttacked(messageAttacked);
        } else if (messageType.Equals(typeof(MessageDamage))) {
            MessageDamage messageDamage = Utils.Deserialize<MessageDamage>(message);
            MessageHandler.Current.onMessageDamage(messageDamage);
        } else if (messageType.Equals(typeof(MessagePickedUp))) {
            MessagePickedUp messagePickedUp = Utils.Deserialize<MessagePickedUp>(message);
            MessageHandler.Current.onMessagePickedUp(messagePickedUp);
        }
    }

    public static void Connect() {
        try {
            _tcpClient = new TcpClient(Config.ServerAddress, Config.ServerPortTCP);
            OnConnected();
            Listen();
        } catch {
            OnConnectionFailed();
        }
    }

    public static async void Send<T>(T message) {
        if (_tcpClient == null || !_tcpClient.Connected) {
            OnError();
        } else {
            string serializedMessage = Utils.Serialize(message);
            byte[] data = Encoding.ASCII.GetBytes(serializedMessage);
            await _tcpClient!.GetStream().WriteAsync(data, 0, data.Length);
        }
    }
}
