using Newtonsoft.Json.Linq;
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
                OnDisconnected();
            }
        }
    }

    private static void OnMessage(string message, Client client) {
        Debug.Log($"[UDPServer] received {message}");
        ClientMessage clientMessage = Utils.ParseJsonString<ClientMessage>(message);

        switch (clientMessage.type) {
            case ClientMessageType.movement:
                ClientMessageMovement messageMovement = Utils.ParseJsonString<ClientMessageMovement>(message);
                OnClientMessageMovement(messageMovement, client);
                break;
        }
    }

    private static void OnClientMessageMovement(ClientMessageMovement messageMovement, Client client) {
        Player player = API.Players.Find(client);
        player.Data.transform = messageMovement.newTransform;
        player.Data.movement = messageMovement.movement;
        player.Avatar.position = messageMovement.newTransform.position.ToVector3();
        player.Avatar.eulerAngles = messageMovement.newTransform.rotation.ToVector3();
    }

    private static void OnStarted() => Debug.Log($"[UDPServer] started");

    private static void OnListening() => Debug.Log($"[UDPServer] listening");

    private static void OnError() => Debug.Log("[UDPServer] not connected");

    private static void OnDisconnected() => Debug.Log("[UDPServer] disconnected");

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

    public void Send(UDPClient recipient, ServerMessage serverMessage) {
        if (_udpClient == null) {
            OnError();
            return;
        }
        JObject obj = JObject.FromObject(serverMessage);
        byte[] messageBytes = Encoding.UTF8.GetBytes(obj.ToString());
        _udpClient.Send(messageBytes, messageBytes.Length, recipient.Address, recipient.Port);
    }
}
