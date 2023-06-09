using Newtonsoft.Json.Linq;
using System;
using UnityEngine;
using WebSocketSharp;

public class TCPClient
{
    private WebSocket ws;
    private readonly string SERVER_ENDPOINT;
    private readonly int SERVER_PORT;

    public TCPClient(string serverEndpoint, int serverPort) {
        SERVER_ENDPOINT = serverEndpoint;
        SERVER_PORT = serverPort;
    }

    public OnServerMessageJoinedGameHandler onServerMessageJoinedGame;

    private void OnOpen(object sender, EventArgs e) {
        Debug.Log("[TCP] connected");
        Authenticate();
    }

    private void OnClose(object sender, EventArgs e) => Debug.Log("Disconnected to master");

    private void OnError(object sender, ErrorEventArgs e) => Debug.Log("Network error " + e.Message);

    private void OnMessage(object sender, MessageEventArgs e) {
        try {
            Debug.Log(e.Data);
            ServerMessage message = JObject.Parse(e.Data).ToObject<ServerMessage>();

            switch (message.type) {
                case ServerMessageType.joinedGame:
                    ServerMessageJoinedGame messageJoinedGame = JObject.Parse(e.Data).ToObject<ServerMessageJoinedGame>();
                    onServerMessageJoinedGame(messageJoinedGame);
                    break;
            }
        } catch (Exception ex) {
            Debug.Log(ex.Message);
        }
    }

    public void Connect(EventHandler onOpenCallback) {
        try {
            ws = new WebSocket($"ws://{SERVER_ENDPOINT}:{SERVER_PORT}");
            ws.OnOpen += OnOpen;
            ws.OnOpen += onOpenCallback;
            ws.OnClose += OnClose;
            ws.OnMessage += OnMessage;
            ws.OnError += OnError;
            ws.Connect();
        } catch {
            Debug.LogError("Connection to the server failed");
        }
    }

    public void DisconnectToServer() => ws.Close();

    public void Authenticate() {
        Send(new ClientMessageAuthenticate());
    }


    public void Send(ClientMessage message) {
        if (!ws.IsAlive) {
            Debug.Log("Socket is not alive");
            return;
        }

        JObject json = JObject.FromObject(message);

        ws.Send(json.ToString());
    }
}
