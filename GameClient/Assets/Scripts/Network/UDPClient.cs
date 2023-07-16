using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public static class UDPClient
{
    private static UdpClient _udpClient;
    private static bool _connected;
    private static int _port = 10000;
    private const string _address = "127.0.0.1";

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

        if (messageType.Equals(typeof(MessagePlayerMoved))) {
            MessagePlayerMoved messagePlayerMoved = Utils.Deserialize<MessagePlayerMoved>(message);
            MessageHandler.Current.OnMessagePlayerMoved?.Invoke(messagePlayerMoved);
        } else if (messageType.Equals(typeof(MessageNPCMoved))) {
            MessageNPCMoved messageNPCMoved = Utils.Deserialize<MessageNPCMoved>(message);
            MessageHandler.Current.OnMessageNPCMoved?.Invoke(messageNPCMoved);
        }
    }

    private static void OnError() => Debug.LogWarning("[UDPClient] not connected");

    private static void OnConnected() => Debug.Log("[UDPClient] connected");

    private static void OnListening() => Debug.Log("[UDPClient] listening on port " + _port);

    public static int Port => _port;

    public static string Address => _address;

    public static void Connect() {
        try {
            _udpClient = new UdpClient();
            _udpClient.Connect(Config.SERVER_ADDRESS, Config.SERVER_PORT_UDP);
            _port = ((IPEndPoint)_udpClient.Client.LocalEndPoint).Port;
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
