using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private LocalPlayer _myPlayer;
    private readonly List<RemotePlayer> _remotePlayers = new List<RemotePlayer>();

    private event OnAddedPlayerHandler _onAddedPlayer;
    private event OnRemovedPlayerHandler _onRemovedPlayer;

    private RemotePlayer FindPlayer(string id) => _remotePlayers.Find((RemotePlayer p) => p.Id == id);

    private void CreatePlayer(PlayerData data) {
        RemotePlayer newRemotePlayer = Instantiate(_playerPrefab, data.transform.position.ToVector3, Quaternion.identity).GetComponent<RemotePlayer>();
        newRemotePlayer.Initialize(data);
        _remotePlayers.Add(newRemotePlayer);
        _onAddedPlayer(newRemotePlayer);
    }

    private void RemovePlayer(RemotePlayer remotePlayer) {
        _onRemovedPlayer(remotePlayer);
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

    private void OnMessageMoved(MessageMoved serverMessageMoved) {
        foreach (PlayerData p in serverMessageMoved.players) {
            if (p.id == _myPlayer.Id)
                continue;

            RemotePlayer remotePlayer = FindPlayer(p.id);
            if (remotePlayer != null) {
                remotePlayer.Movement.DestinationPosition = p.transform.position.ToVector3;
                remotePlayer.Movement.DestinationRotation = p.transform.rotation.ToVector3;
                remotePlayer.Movement.AnimationData = p.animation;
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

        RemotePlayer remotePlayer = FindPlayer(messageAttacked.id);
        if (remotePlayer != null)
            remotePlayer.Attack.Attack();

    }

    private void OnMessageDamage(MessageDamage messageDamage) {
        if (messageDamage.idTo == _myPlayer.Id)
            _myPlayer.Health.TakeDamage(10);
        else {
            RemotePlayer remotePlayer = FindPlayer(messageDamage.idTo);
            if (remotePlayer != null)
                remotePlayer.Health.TakeDamage(10);
        }
    }

    private void Awake() {
        MessageHandler.OnMessageGameStateEvent += OnMessageGameState;
        MessageHandler.OnMessageJoinedGameEvent += OnMessageJoinedGame;
        MessageHandler.OnMessageLeftGameEvent += OnMessageLeftGame;
        MessageHandler.OnMessageMovedEvent += OnMessageMoved;
        MessageHandler.OnMessageAttackedEvent += OnMessageAttacked;
        MessageHandler.OnMessageDamageEvent += OnMessageDamage;
    }

    public delegate void OnAddedPlayerHandler(Player player);
    public delegate void OnRemovedPlayerHandler(Player player);

    public event OnAddedPlayerHandler OnAddedPlayerEvent {
        add => _onAddedPlayer += value;
        remove => _onAddedPlayer -= value;
    }

    public event OnRemovedPlayerHandler OnRemovedPlayerEvent {
        add => _onRemovedPlayer += value;
        remove => _onRemovedPlayer -= value;
    }

    public LocalPlayer MyPlayer => _myPlayer;
}