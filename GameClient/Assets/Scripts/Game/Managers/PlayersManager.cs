using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    private static PlayersManager _current;
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private LocalPlayer _myPlayer;
    private readonly List<RemotePlayer> _remotePlayers = new List<RemotePlayer>();
    private event OnAddedPlayerHandler _onAddedPlayer;
    private event OnRemovedPlayerHandler _onRemovedPlayer;

    private RemotePlayer FindPlayer(string id) => _remotePlayers.Find((RemotePlayer p) => p.Id == id);

    private void CreatePlayer(PlayerData data) {
        RemotePlayer newRemotePlayer = Instantiate(_playerPrefab, data.transformData.position.ToVector3, Quaternion.identity).GetComponent<RemotePlayer>();
        newRemotePlayer.Initialize(data.id);
        _remotePlayers.Add(newRemotePlayer);
        _onAddedPlayer?.Invoke(newRemotePlayer);
    }

    private void RemovePlayer(RemotePlayer remotePlayer) {
        _onRemovedPlayer?.Invoke(remotePlayer);
        _remotePlayers.Remove(remotePlayer);
        Destroy(remotePlayer.gameObject);
    }

    private void OnMessageGameState(MessageGameState messageGameState) {
        _myPlayer.Initialize(messageGameState.id);
        foreach (PlayerData p in messageGameState.players) {
            if (p.id != _myPlayer.Id)
                CreatePlayer(p);
        }
    }

    private void OnMessageJoinedGame(MessageJoinedGame messageJoinedGame) {
        if (messageJoinedGame.player.id != _myPlayer.Id)
            CreatePlayer(messageJoinedGame.player);
    }

    private void OnMessagePlayerMoved(MessagePlayerMoved serverMessagePlayerMoved) {
        foreach (PlayerData p in serverMessagePlayerMoved.players) {
            if (p.id == _myPlayer.Id)
                continue;

            RemotePlayer remotePlayer = FindPlayer(p.id);
            if (remotePlayer != null) {
                remotePlayer.Movement.DestinationPosition = p.transformData.position.ToVector3;
                remotePlayer.Movement.DestinationRotation = p.transformData.rotation.ToVector3;
                remotePlayer.Movement.PlayerAnimationData = p.animationData;
            }
        }
    }

    private void OnMessageLeftGame(MessageLeftGame serverMessageLeftGame) {
        if (serverMessageLeftGame.id == _myPlayer.Id)
            return;

        RemotePlayer remotePlayer = FindPlayer(serverMessageLeftGame.id);
        if (remotePlayer != null)
            RemovePlayer(remotePlayer);
    }

    private void OnMessageAttacked(MessageAttacked messageAttacked) {
        if (messageAttacked.id == _myPlayer.Id)
            return;

        FindPlayer(messageAttacked.id)?.TriggerAnimation("Attack");
    }

    private void OnMessageHealthChanged(MessageHealthChanged messageHealthChanged) {
        if (messageHealthChanged.id == _myPlayer.Id) {
            _myPlayer.Statistics.MaxHealth = messageHealthChanged.maxHealth;
            _myPlayer.Statistics.CurrentHealth = messageHealthChanged.currentHealth;
        } else {
            RemotePlayer remotePlayer = FindPlayer(messageHealthChanged.id);
            if (remotePlayer != null) {
                remotePlayer.Statistics.MaxHealth = messageHealthChanged.maxHealth;
                remotePlayer.Statistics.CurrentHealth = messageHealthChanged.currentHealth;
            }
        }
    }

    private void OnMessageChannel(MessageChannel messageChannel) {
        if (messageChannel.id == _myPlayer.Id)
            _myPlayer.Channel(messageChannel.activityName, messageChannel.ticks, messageChannel.intervalTimeInSeconds);
        else
            FindPlayer(messageChannel.id)?.Channel(messageChannel.activityName, messageChannel.ticks, messageChannel.intervalTimeInSeconds);
    }

    private void OnMessageCast(MessageCast messageCast) {
        if (messageCast.id == _myPlayer.Id)
            _myPlayer.Cast(messageCast.activityName, messageCast.castTimeInSeconds);
        else
            FindPlayer(messageCast.id)?.Cast(messageCast.activityName, messageCast.castTimeInSeconds);
    }

    private void OnMessageStopActivity(MessageStopActivity messageStopActivity) {
        if (messageStopActivity.id == _myPlayer.Id)
            _myPlayer.StopActivity();
        else
            FindPlayer(messageStopActivity.id)?.StopActivity();
    }

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
        MessageHandler.Current.OnMessageGameStateEvent += OnMessageGameState;
        MessageHandler.Current.OnMessageJoinedGameEvent += OnMessageJoinedGame;
        MessageHandler.Current.OnMessageLeftGameEvent += OnMessageLeftGame;
        MessageHandler.Current.OnMessagePlayerMovedEvent += OnMessagePlayerMoved;
        MessageHandler.Current.OnMessageAttackedEvent += OnMessageAttacked;
        MessageHandler.Current.OnMessageHealthChangedEvent += OnMessageHealthChanged;
        MessageHandler.Current.OnMessageChannelEvent += OnMessageChannel;
        MessageHandler.Current.OnMessageCastEvent += OnMessageCast;
        MessageHandler.Current.OnMessageStopActivityEvent += OnMessageStopActivity;
    }

    public delegate void OnAddedPlayerHandler(Character player);
    public delegate void OnRemovedPlayerHandler(Character player);

    public static PlayersManager Current => _current;

    public event OnAddedPlayerHandler OnAddedPlayerEvent {
        add => _onAddedPlayer += value;
        remove => _onAddedPlayer -= value;
    }

    public event OnRemovedPlayerHandler OnRemovedPlayerEvent {
        add => _onRemovedPlayer += value;
        remove => _onRemovedPlayer -= value;
    }
}
