using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public static class TCPClient
{
    private static TcpClient _tcpClient;

    public static MessageHandler MessageHandler { get; } = new MessageHandler();

    private static async void Listen() {
        if (_tcpClient == null || !_tcpClient.Connected) {
            OnError();
        }
        OnListen();
        NetworkStream stream = _tcpClient!.GetStream();

        byte[] bytes = new byte[SharedConfig.Current.TCPBatchSize];
        int i;
        string buffer = "";

        try {
            while ((i = await stream.ReadAsync(bytes, 0, bytes.Length)) != 0) {
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
            OnDisconnected();
        } catch (SocketException e) {
            Debug.LogException(e);
            OnDisconnected();
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }

    private static void OnError() {
        Debug.Log($"[TCP Client] not connected");
    }

    private static void OnListen() {
        Debug.Log($"[TCP Client] listening");
    }

    private static void OnDisconnected() {
        Debug.Log($"[TCP Client] client disconnected");
        SceneLoader.Current.LoadMenuAsync();
    }

    private static void OnConnectionFailed() {
        Debug.Log($"[TCP Client] connection failed");
    }

    private static void OnConnected() {
        Debug.Log($"[TCP Client] connected");
    }

    private static void OnMessage(string message) {
        Debug.Log($"[TCP Client] received {message}");

        Type messageType = Utils.GetMessageType(message);
        object obj = Utils.Deserialize(message, messageType);

        MessageHandler.Invoke(obj, messageType);
    }

    public static void Connect() {
        try {
            _tcpClient = new TcpClient(Config.Current.ServerAddress, Config.Current.ServerPortTCP);
            OnConnected();
            Listen();
        } catch {
            OnConnectionFailed();
        }
    }

    public static async void Send<T>(T message) {
        if (_tcpClient == null || !_tcpClient.Connected) {
            OnError();
        } else {
            string serializedMessage = Utils.Serialize(message);
            serializedMessage += '#';
            byte[] bytes = Encoding.ASCII.GetBytes(serializedMessage);

            int batchSize = SharedConfig.Current.TCPBatchSize;
            int i = 0;

            while (i < bytes.Length) {
                if (i + batchSize > bytes.Length) {
                    await _tcpClient!.GetStream().WriteAsync(bytes, i, bytes.Length - i);
                    i = bytes.Length;
                } else {
                    await _tcpClient!.GetStream().WriteAsync(bytes, i, batchSize);
                    i += batchSize;
                }
            }
        }
    }
}
