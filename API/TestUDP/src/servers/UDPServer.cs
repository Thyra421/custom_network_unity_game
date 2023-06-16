using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using System.Text;

namespace TestUDP;

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
                Player? player = API.Players.Find(result.RemoteEndPoint.Address.ToString(), result.RemoteEndPoint.Port);
                if (player != null)
                    OnMessage(message, player);
            } catch (Exception) {
                OnDisconnected();
            }
        }
    }

    private static void OnMessage(string message, Player player) {
        Console.WriteLine($"[UDPServer] received {message}");
        ClientMessage clientMessage = Utils.ParseJsonString<ClientMessage>(message);

        switch (clientMessage.type) {
            case ClientMessageType.movement:
                ClientMessageMovement messageMovement = Utils.ParseJsonString<ClientMessageMovement>(message);
                OnClientMessageMovement(messageMovement, player);
                break;
        }
    }

    private static void OnClientMessageMovement(ClientMessageMovement messageMovement, Player player) {
        player.Data.transform = messageMovement.transform;
        player.Data.movement = messageMovement.movement;
    }

    private static void OnConnected() => Console.WriteLine($"[UDPServer] connected");

    private static void OnListening() => Console.WriteLine($"[UDPServer] listening");

    private static void OnError() => Console.WriteLine("[UDPServer] not connected");

    private static void OnDisconnected() => Console.WriteLine("[UDPServer] disconnected");

    public void Start() {
        _udpClient = new UdpClient(Config.UDPPort);
        OnConnected();
        Listen();
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
