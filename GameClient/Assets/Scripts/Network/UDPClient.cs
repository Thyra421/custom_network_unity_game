using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public static class UDPClient
{
    private static UdpClient _udpClient;
    private static bool _connected;

    public static MessageHandler MessageHandler { get; } = new MessageHandler();
    public static string Address { get; private set; }
    public static int Port { get; private set; }

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
        Type messageType = Utils.GetMessageType(message);
        object obj = Utils.Deserialize(message, messageType);
        MessageHandler.Invoke(obj, messageType);
    }

    private static void OnError() => Debug.LogWarning("[UDPClient] not connected");

    private static void OnConnected() => Debug.Log("[UDPClient] connected");

    private static void OnListening() => Debug.Log("[UDPClient] listening on port " + Port);

    public static void Connect() {
        try {
            _udpClient = new UdpClient();
            _udpClient.Connect(Config.Current.ServerAddress, Config.Current.ServerPortUDP);
            Port = ((IPEndPoint)_udpClient.Client.LocalEndPoint).Port;
            Address = ((IPEndPoint)_udpClient.Client.LocalEndPoint).Address.ToString();
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
