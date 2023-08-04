using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPServer
{
    private UdpClient _udpClient;

    private async void Listen() {
        if (_udpClient == null) {
            OnError();
            return;
        }
        OnListening();
        while (true) {
            try {
                UdpReceiveResult result = await _udpClient.ReceiveAsync();
                string message = Encoding.UTF8.GetString(result.Buffer);
                Client client = API.Clients.Find(result.RemoteEndPoint.Address.ToString(), result.RemoteEndPoint.Port);
                if (client != null)
                    OnMessage(message, client);
            } catch (Exception e) {
                Debug.LogException(e);
            }
        }
    }

    private static void OnStarted() => Debug.Log($"[UDPServer] started");

    private static void OnListening() => Debug.Log($"[UDPServer] listening");

    private static void OnError() => Debug.Log("[UDPServer] not connected");

    private static void OnConnectionFailed() => Debug.Log($"[UDPServer] connection failed");

    private void OnMessage(string message, Client client) {
        Type messageType = Utils.GetMessageType(message);
        object obj = Utils.Deserialize(message, messageType);
        client.UDP.MessageHandler.Invoke(obj, messageType);
    }

    public bool Start() {
        try {
            _udpClient = new UdpClient(Config.Current.UDPPort);
            OnStarted();
            Listen();
        } catch (Exception) {
            OnConnectionFailed();
            return false;
        }
        return true;
    }

    /// <summary>
    /// Prepares the message and sends it.
    /// Use SendBytes for better performance when broadcasting the same message to several clients to avoid re-serializing it each time.
    /// </summary>
    public void Send<T>(UDPClient recipient, T message) {
        if (_udpClient == null) {
            OnError();
            return;
        }

        byte[] messageBytes = Utils.GetBytes(message);
        SendBytes(recipient, messageBytes);
    }

    public void SendBytes(UDPClient recipient, byte[] messageBytes) {
        if (_udpClient == null) {
            OnError();
            return;
        }

        _udpClient.SendAsync(messageBytes, messageBytes.Length, recipient.Address, recipient.Port);
    }
}
