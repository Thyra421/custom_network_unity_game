using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TCPClient
{
    private readonly NetworkStream _stream;

    private Player Player => Client.Player;
    private Room Room => Player.Room;

    public MessageHandler MessageHandler { get; } = new MessageHandler();
    public Client Client { get; set; }

    private void OnDisconnect() {
        Debug.Log($"[TCPClient] disconnected");

        if (Player != null)
            Room.PlayersManager.RemovePlayer(Player);

        API.Clients.Remove(Client);
    }

    private void OnMessage(string message) {
        Debug.Log($"[TCPClient] received {message}");

        Type messageType = Utils.GetMessageType(message);
        object obj = Utils.Deserialize(message, messageType);
        MessageHandler.Invoke(obj, messageType);
    }

    private async void Listen() {
        byte[] bytes = new byte[SharedConfig.Current.TCPBatchSize];
        int i;
        string buffer = "";

        try {
            while ((i = await _stream.ReadAsync(bytes, 0, bytes.Length)) != 0) {
                string message = Encoding.ASCII.GetString(bytes, 0, i);
                while (message.Contains('#')) {
                    int endIndex = message.IndexOf('#');
                    buffer += message[..endIndex];
                    OnMessage(buffer);
                    buffer = "";
                    if (endIndex < message.Length)
                        message = message[(endIndex + 1)..];
                }
                buffer += message;
            }
            OnDisconnect();
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }

    public TCPClient(TcpClient tcpClient) {
        _stream = tcpClient.GetStream();
        Listen();
    }

    /// <summary>
    /// Prepares the message and sends it.
    /// Use SendBytes for better performance when broadcasting the same message to several clients to avoid re-serializing it each time.
    /// </summary>
    public void Send<T>(T message) {
        byte[] bytes = Utils.GetBytesForTCP(message);

        SendBytes(bytes);
    }

    public void SendBytes(byte[] bytes) {
        int batchSize = SharedConfig.Current.TCPBatchSize;
        int i = 0;

        while (i < bytes.Length) {
            if (i + batchSize >= bytes.Length) {
                _stream.Write(bytes, i, bytes.Length - i);
                i = bytes.Length;
            } else {
                _stream.Write(bytes, i, batchSize);
                i += batchSize;
            }
        }
    }
}
