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
        } else if (messageType.Equals(typeof(MessageUsedAbility))) {
            MessageUsedAbility messageUsedAbility = Utils.Deserialize<MessageUsedAbility>(message);
            MessageHandler.Current.OnMessageUsedAbility?.Invoke(messageUsedAbility);
        } else if (messageType.Equals(typeof(MessageDespawnNode))) {
            MessageDespawnNode messageDespawnNode = Utils.Deserialize<MessageDespawnNode>(message);
            MessageHandler.Current.OnMessageDespawnNode?.Invoke(messageDespawnNode);
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
        } else if (messageType.Equals(typeof(MessageEquiped))) {
            MessageEquiped messageEquiped = Utils.Deserialize<MessageEquiped>(message);
            MessageHandler.Current.OnMessageEquiped?.Invoke(messageEquiped);
        } else if (messageType.Equals(typeof(MessageSpawnVFX))) {
            MessageSpawnVFX messageSpawnVFX = Utils.Deserialize<MessageSpawnVFX>(message);
            MessageHandler.Current.OnMessageSpawnVFX?.Invoke(messageSpawnVFX);
        } else if (messageType.Equals(typeof(MessageDespawnVFX))) {
            MessageDespawnVFX messageDespawnVFX = Utils.Deserialize<MessageDespawnVFX>(message);
            MessageHandler.Current.OnMessageDespawnVFX?.Invoke(messageDespawnVFX);
        } else if (messageType.Equals(typeof(MessageTriggerAnimation))) {
            MessageTriggerAnimation messageTriggerAnimation = Utils.Deserialize<MessageTriggerAnimation>(message);
            MessageHandler.Current.OnMessageTriggerAnimation?.Invoke(messageTriggerAnimation);
        } else if (messageType.Equals(typeof(MessageAddAlteration))) {
            MessageAddAlteration messageAddAlteration = Utils.Deserialize<MessageAddAlteration>(message);
            MessageHandler.Current.OnMessageAddAlteration?.Invoke(messageAddAlteration);
        } else if (messageType.Equals(typeof(MessageRefreshAlteration))) {
            MessageRefreshAlteration messageRefreshAlteration = Utils.Deserialize<MessageRefreshAlteration>(message);
            MessageHandler.Current.OnMessageRefreshAlteration?.Invoke(messageRefreshAlteration);
        } else if (messageType.Equals(typeof(MessageRemoveAlteration))) {
            MessageRemoveAlteration messageRemoveAlteration = Utils.Deserialize<MessageRemoveAlteration>(message);
            MessageHandler.Current.OnMessageRemoveAlteration?.Invoke(messageRemoveAlteration);
        } else if (messageType.Equals(typeof(MessageStatisticsChanged))) {
            MessageStatisticsChanged messageStatisticsChanged = Utils.Deserialize<MessageStatisticsChanged>(message);
            MessageHandler.Current.OnMessageStatisticsChanged?.Invoke(messageStatisticsChanged);
        } else if (messageType.Equals(typeof(MessageStatesChanged))) {
            MessageStatesChanged messageStatesChanged = Utils.Deserialize<MessageStatesChanged>(message);
            MessageHandler.Current.OnMessageStatesChanged?.Invoke(messageStatesChanged);
        }
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
