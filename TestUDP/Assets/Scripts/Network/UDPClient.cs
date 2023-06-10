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

    public OnServerMessagePositionsHandler onServerMessagePositions;

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
            string msg = Encoding.UTF8.GetString(result.Buffer);
            Debug.Log(msg);

            ServerMessage message = Utils.ParseJsonString<ServerMessage>(msg);

            switch (message.type) {
                case ServerMessageType.positions:
                    ServerMessagePositions messagePositions = Utils.ParseJsonString<ServerMessagePositions>(msg);
                    onServerMessagePositions(messagePositions);
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
