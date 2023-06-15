using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPClient
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
            } catch (Exception) {
                OnDisconnected();
            }
        }
    }

    private static void OnMessage(string message) {
        Debug.Log($"[UDPServer] received {message}");
        ServerMessage serverMessage = Utils.ParseJsonString<ServerMessage>(message);

        switch (serverMessage.type) {
            case ServerMessageType.movements:
                ServerMessageMovements messageMovements = Utils.ParseJsonString<ServerMessageMovements>(message);
                MessageHandler.Current.onServerMessageMovements(messageMovements);
                break;
        }
    }

    private static void OnError() => Debug.LogError("[UDPClient] not connected");

    private static void OnConnected() => Debug.Log("[UDPClient] connected");

    private static void OnListening() => Debug.Log("[UDPClient] listening on port " + _port);

    private static void OnDisconnected() => Debug.Log("[UDPClient] disconnected");

    public static int Port => _port;

    public static string Address => _address;

    public static void Connect() {
        try {
            _udpClient = new UdpClient();
            _udpClient.Connect(Config.ServerAddress, Config.ServerPortUDP);
            _port = ((IPEndPoint)_udpClient.Client.LocalEndPoint).Port;
            _connected = true;
            OnConnected();
            Listen();
        } catch {
            Connect();
        }
    }

    public static void Send(ClientMessage message) {
        if (!_connected) {
            OnError();
            return;
        }
        JObject json = JObject.FromObject(message);
        byte[] messageBytes = Encoding.UTF8.GetBytes(json.ToString());
        _udpClient.Send(messageBytes, messageBytes.Length);
    }
}
