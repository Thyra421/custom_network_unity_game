using Newtonsoft.Json.Linq;
using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPClient
{
    private UdpClient udp;
    private int PORT = 10000;
    private readonly string SERVER_ENDPOINT;
    private readonly int SERVER_PORT;
    private bool connected;

    public OnServerMessagePositionHandler onServerMessagePosition;

    public UDPClient(string serverEndpoint, int serverPort) {
        SERVER_ENDPOINT = serverEndpoint;
        SERVER_PORT = serverPort;
    }

    public void Connect(Action onConnectedCallback) {
        try {
            udp = new UdpClient(PORT);
            udp.Connect(SERVER_ENDPOINT, SERVER_PORT);
            connected = true;
            onConnectedCallback();
            Debug.Log("[UDP] connected");
            Listen();
        } catch (Exception ex) {
            Debug.Log(ex.Message);
            PORT++;
            Connect(onConnectedCallback);
        }
    }

    async void Listen() {
        while (true) {
            UdpReceiveResult result = await udp.ReceiveAsync();
            Debug.Log(Encoding.UTF8.GetString(result.Buffer));
            JObject json = JObject.Parse(Encoding.UTF8.GetString(result.Buffer));

            ServerMessage message = json.ToObject<ServerMessage>();

            switch (message.type) {
                case ServerMessageType.position:
                    ServerMessagePosition messagePosition = json.ToObject<ServerMessagePosition>();
                    onServerMessagePosition(messagePosition);
                    break;
            }
        }
    }

    public void Send(ClientMessage message) {
        if (!connected)
            Debug.LogError("[UDP] not connected");
        JObject json = JObject.FromObject(message);
        byte[] messageBytes = Encoding.UTF8.GetBytes(json.ToString());
        udp.Send(messageBytes, messageBytes.Length);
    }
}
