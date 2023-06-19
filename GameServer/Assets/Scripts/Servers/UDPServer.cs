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

    private static void OnMessage(string message, Client client) {
        Debug.Log($"[UDPServer] received {message}");
        Type messageType = Utils.GetMessageType(message);

        if (messageType.Equals(typeof(MessageMovement))) {
            MessageMovement messageMovement = Utils.Deserialize<MessageMovement>(message);
            OnMessageMovement(messageMovement, client);
        }
    }

    private static void OnMessageMovement(MessageMovement messageMovement, Client client) {
        client.Player.TransformData = messageMovement.newTransform;
        client.Player.AnimationData = messageMovement.movement;
    }

    private static void OnStarted() => Debug.Log($"[UDPServer] started");

    private static void OnListening() => Debug.Log($"[UDPServer] listening");

    private static void OnError() => Debug.Log("[UDPServer] not connected");

    private static void OnConnectionFailed() => Debug.Log($"[UDPServer] connection failed");

    public bool Start() {
        try {
            _udpClient = new UdpClient(Config.UDPPort);
            OnStarted();
            Listen();
        } catch (Exception) {
            OnConnectionFailed();
            return false;
        }
        return true;
    }

    public void Send<T>(UDPClient recipient, T message) {
        if (_udpClient == null) {
            OnError();
            return;
        }
        string serializedMessage = Utils.Serialize(message);
        byte[] messageBytes = Encoding.UTF8.GetBytes(serializedMessage);
        _udpClient.Send(messageBytes, messageBytes.Length, recipient.Address, recipient.Port);
    }
}
