using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public static class TCPClient
{
    private static TcpClient _tcpClient;

    private static async void Listen() {
        if (_tcpClient == null || !_tcpClient.Connected) {
            OnError();
        }
        OnListen();
        NetworkStream stream = _tcpClient!.GetStream();

        byte[] bytes = new byte[SharedConfig.TCP_BATCH_SIZE];
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

        if (messageType.Equals(typeof(MessageJoinedGame))) {
            MessageJoinedGame messageJoinedGame = Utils.Deserialize<MessageJoinedGame>(message);
            MessageHandler.Current.OnMessageJoinedGame?.Invoke(messageJoinedGame);
        } else if (messageType.Equals(typeof(MessageLeftGame))) {
            MessageLeftGame messageLeftGame = Utils.Deserialize<MessageLeftGame>(message);
            MessageHandler.Current.OnMessageLeftGame?.Invoke(messageLeftGame);
        } else if (messageType.Equals(typeof(MessageGameState))) {
            MessageGameState messageGameState = Utils.Deserialize<MessageGameState>(message);
            MessageHandler.Current.OnMessageGameState?.Invoke(messageGameState);
        } else if (messageType.Equals(typeof(MessageSpawnNodes))) {
            MessageSpawnNodes messageSpawnNodes = Utils.Deserialize<MessageSpawnNodes>(message);
            MessageHandler.Current.OnMessageSpawnNodes?.Invoke(messageSpawnNodes);
        } else if (messageType.Equals(typeof(MessageSpawnNPCs))) {
            MessageSpawnNPCs messageSpawnNPCs = Utils.Deserialize<MessageSpawnNPCs>(message);
            MessageHandler.Current.OnMessageSpawnNPCs?.Invoke(messageSpawnNPCs);
        }  else if (messageType.Equals(typeof(MessageAttacked))) {
            MessageAttacked messageAttacked = Utils.Deserialize<MessageAttacked>(message);
            MessageHandler.Current.OnMessageAttacked?.Invoke(messageAttacked);
        } else if (messageType.Equals(typeof(MessageDespawnObject))) {
            MessageDespawnObject messageDespawnObject = Utils.Deserialize<MessageDespawnObject>(message);
            MessageHandler.Current.OnMessageDespawnObject?.Invoke(messageDespawnObject);
        } else if (messageType.Equals(typeof(MessageInventoryAdd))) {
            MessageInventoryAdd messageInventoryAdd = Utils.Deserialize<MessageInventoryAdd>(message);
            MessageHandler.Current.OnMessageInventoryAdd?.Invoke(messageInventoryAdd);
        } else if (messageType.Equals(typeof(MessageInventoryRemove))) {
            MessageInventoryRemove messageInventoryRemove = Utils.Deserialize<MessageInventoryRemove>(message);
            MessageHandler.Current.OnMessageInventoryRemove?.Invoke(messageInventoryRemove);
        } else if (messageType.Equals(typeof(MessageCrafted))) {
            MessageCrafted messageCrafted = Utils.Deserialize<MessageCrafted>(message);
            MessageHandler.Current.OnMessageCrafted?.Invoke(messageCrafted);
        } else if (messageType.Equals(typeof(MessageHealthChanged))) {
            MessageHealthChanged messageHealthChanged = Utils.Deserialize<MessageHealthChanged>(message);
            MessageHandler.Current.OnMessageHealthChanged?.Invoke(messageHealthChanged);
        } else if (messageType.Equals(typeof(MessageChannel))) {
            MessageChannel messageChannel = Utils.Deserialize<MessageChannel>(message);
            MessageHandler.Current.OnMessageChannel?.Invoke(messageChannel);
        } else if (messageType.Equals(typeof(MessageCast))) {
            MessageCast messageCast = Utils.Deserialize<MessageCast>(message);
            MessageHandler.Current.OnMessageCast?.Invoke(messageCast);
        } else if (messageType.Equals(typeof(MessageStopActivity))) {
            MessageStopActivity messageStopActivity = Utils.Deserialize<MessageStopActivity>(message);
            MessageHandler.Current.OnMessageStopActivity?.Invoke(messageStopActivity);
        } else if (messageType.Equals(typeof(MessageExperienceChanged))) {
            MessageExperienceChanged messageExperienceChanged = Utils.Deserialize<MessageExperienceChanged>(message);
            MessageHandler.Current.OnMessageExperienceChanged?.Invoke(messageExperienceChanged);
        }
    }

    public static void Connect() {
        try {
            _tcpClient = new TcpClient(Config.SERVER_ADDRESS, Config.SERVER_PORT_TCP);
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

            int batchSize = SharedConfig.TCP_BATCH_SIZE;
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
