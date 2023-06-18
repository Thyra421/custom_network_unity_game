using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private LocalPlayer _myPlayer;
    private readonly List<RemotePlayer> _remotePlayers = new List<RemotePlayer>();

    private void CreatePlayer(ObjectData player) {
        MainThreadWorker.Current.AddJob(() => {
            GameObject newPlayer = Instantiate(_playerPrefab, player.transform.position.ToVector3(), Quaternion.identity);
            RemotePlayer remotePlayer = newPlayer.GetComponent<RemotePlayer>();
            remotePlayer.Id = player.id;
            _remotePlayers.Add(remotePlayer);
        });
    }

    private void OnMessageGameState(MessageGameState messageGameState) {
        _myPlayer.Id = messageGameState.id;
        foreach (ObjectData p in messageGameState.players) {
            if (p.id != _myPlayer.Id)
                CreatePlayer(p);
        }
    }

    private void OnMessageJoinedGame(MessageJoinedGame messageJoinedGame) {
        if (messageJoinedGame.player.id != _myPlayer.Id)
            CreatePlayer(messageJoinedGame.player);
    }

    private void OnMessageMovements(MessageMovements serverMessageMovements) {
        foreach (ObjectData p in serverMessageMovements.players) {
            if (p.id == _myPlayer.Id)
                continue;

            RemotePlayer remotePlayer = _remotePlayers.Find((RemotePlayer rp) => rp.Id == p.id);
            if (remotePlayer == null)
                continue;

            remotePlayer.Movement.DestinationPosition = p.transform.position.ToVector3();
            remotePlayer.Movement.DestinationRotation = p.transform.rotation.ToVector3();
            remotePlayer.Movement.MovementData = p.movement;
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

    private void OnMessagePlayerAttack(MessagePlayerAttack messagePlayerAttack) {
        if (messagePlayerAttack.id == _myPlayer.Id)
            return;

        RemotePlayer remotePlayer = _remotePlayers.Find((RemotePlayer rp) => rp.Id == messagePlayerAttack.id);
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
        MessageHandler.Current.onMessagePlayerAttack += OnMessagePlayerAttack;
        MessageHandler.Current.onMessageDamage += OnMessageDamage;
        TCPClient.Send(new MessagePlay());
    }
}