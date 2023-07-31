using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public static class UDPClient
{
    public const string Address = "127.0.0.1";
    private static UdpClient _udpClient;
    private static bool _connected;

    public static int Port { get; private set; } = 10000;

    private static async void Listen() {
        if (!_connected) {
            OnError();
            return;
        }
        OnListening();

        while (true) {
            try {
                UdpReceiveResult result = await _udpClient.ReceiveAsync();
                string message = Encoding.UTF8.GetString(result.Buffer);
                OnMessage(message);
            } catch (Exception e) {
                Debug.LogException(e);
            }
        }
    }

    private static void OnMessage(string message) {
        //Debug.Log($"[UDPServer] received {message}");
        Type messageType = Utils.GetMessageType(message);

        if (messageType.Equals(typeof(MessagePlayersMoved))) {
            MessagePlayersMoved messagePlayersMoved = Utils.Deserialize<MessagePlayersMoved>(message);
            MessageHandler.Current.OnMessagePlayersMoved?.Invoke(messagePlayersMoved);
        } else if (messageType.Equals(typeof(MessageNPCsMoved))) {
            MessageNPCsMoved messageNPCsMoved = Utils.Deserialize<MessageNPCsMoved>(message);
            MessageHandler.Current.OnMessageNPCsMoved?.Invoke(messageNPCsMoved);
        } else if (messageType.Equals(typeof(MessageVFXsMoved))) {
            MessageVFXsMoved messageVFXsMoved = Utils.Deserialize<MessageVFXsMoved>(message);
            MessageHandler.Current.OnMessageVFXsMoved?.Invoke(messageVFXsMoved);
        }
    }

    private static void OnError() => Debug.LogWarning("[UDPClient] not connected");

    private static void OnConnected() => Debug.Log("[UDPClient] connected");

    private static void OnListening() => Debug.Log("[UDPClient] listening on port " + Port);

    public static void Connect() {
        try {
            _udpClient = new UdpClient();
            _udpClient.Connect(Config.Current.ServerAddress, Config.Current.ServerPortUDP);
            Port = ((IPEndPoint)_udpClient.Client.LocalEndPoint).Port;
            _connected = true;
            OnConnected();
            Listen();
        } catch {
            Connect();
        }
    }

    public static void Send<T>(T message) {
        if (!_connected) {
            OnError();
            return;
        }
        string serializedMessage = Utils.Serialize(message);
        byte[] messageBytes = Encoding.UTF8.GetBytes(serializedMessage);
        _udpClient.Send(messageBytes, messageBytes.Length);
    }
}
