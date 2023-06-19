using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private LocalPlayer _myPlayer;
    private readonly List<RemotePlayer> _remotePlayers = new List<RemotePlayer>();

    private void CreatePlayer(PlayerData player) {
        MainThreadWorker.Current.AddJob(() => {
            GameObject newPlayer = Instantiate(_playerPrefab, player.transform.position.ToVector3, Quaternion.identity);
            RemotePlayer remotePlayer = newPlayer.GetComponent<RemotePlayer>();
            remotePlayer.Id = player.id;
            _remotePlayers.Add(remotePlayer);
        });
    }

    private void OnMessageGameState(MessageGameState messageGameState) {
        _myPlayer.Id = messageGameState.id;
        foreach (PlayerData p in messageGameState.players) {
            if (p.id != _myPlayer.Id)
                CreatePlayer(p);
        }
    }

    private void OnMessageJoinedGame(MessageJoinedGame messageJoinedGame) {
        if (messageJoinedGame.player.id != _myPlayer.Id)
            CreatePlayer(messageJoinedGame.player);
    }

    private void OnMessageMovements(MessageMovements serverMessageMovements) {
        foreach (PlayerData p in serverMessageMovements.players) {
            if (p.id == _myPlayer.Id)
                continue;

            RemotePlayer remotePlayer = _remotePlayers.Find((RemotePlayer rp) => rp.Id == p.id);
            if (remotePlayer == null)
                continue;

            remotePlayer.Movement.DestinationPosition = p.transform.position.ToVector3;
            remotePlayer.Movement.DestinationRotation = p.transform.rotation.ToVector3;
            remotePlayer.Movement.AnimationData = p.animation;
        }
    }

    private void OnMessageLeftGame(MessageLeftGame serverMessageLeftGame) {
        if (serverMessageLeftGame.id == _myPlayer.Id)
            return;

        RemotePlayer remotePlayer = _remotePlayers.Find((RemotePlayer rp) => rp.Id == serverMessageLeftGame.id);
        if (remotePlayer != null) {
            _remotePlayers.Remove(remotePlayer);
            Destroy(remotePlayer.gameObject);
        }
    }

    private void OnMessageAttacked(MessageAttacked messageAttacked) {
        if (messageAttacked.id == _myPlayer.Id)
            return;

        RemotePlayer remotePlayer = _remotePlayers.Find((RemotePlayer rp) => rp.Id == messageAttacked.id);
        if (remotePlayer != null) {
            remotePlayer.Attack.Attack();
        }
    }

    private void OnMessageDamage(MessageDamage messageDamage) {
        if (messageDamage.idTo == _myPlayer.Id)
            _myPlayer.Health.TakeDamage(10);
        else {
            RemotePlayer remotePlayer = _remotePlayers.Find((RemotePlayer rp) => rp.Id == messageDamage.idTo);
            if (remotePlayer != null) {
                remotePlayer.Health.TakeDamage(10);
            }
        }
    }

    private void Start() {
        MessageHandler.Current.onMessageGameState += OnMessageGameState;
        MessageHandler.Current.onMessageJoinedGame += OnMessageJoinedGame;
        MessageHandler.Current.onMessageMovements += OnMessageMovements;
        MessageHandler.Current.onMessageLeftGame += OnMessageLeftGame;
        MessageHandler.Current.onMessageAttacked += OnMessageAttacked;
        MessageHandler.Current.onMessageDamage += OnMessageDamage;
        TCPClient.Send(new MessagePlay());
    }

    public LocalPlayer MyPlayer => _myPlayer;
}