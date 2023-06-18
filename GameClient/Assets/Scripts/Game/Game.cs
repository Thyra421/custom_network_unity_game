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

    private void OnServerMessageGameState(ServerMessageGameState messageGameState) {
        _myPlayer.Id = messageGameState.id;
        foreach (ObjectData p in messageGameState.players) {
            if (p.id != _myPlayer.Id)
                CreatePlayer(p);
        }
    }

    private void OnServerMessageJoinedGame(ServerMessageJoinedGame messageJoinedGame) {
        if (messageJoinedGame.player.id != _myPlayer.Id)
            CreatePlayer(messageJoinedGame.player);
    }

    private void OnServerMessageMovements(ServerMessageMovements serverMessageMovements) {
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

    private void OnServerMessageLeftGame(ServerMessageLeftGame serverMessageLeftGame) {
        if (serverMessageLeftGame.id == _myPlayer.Id)
            return;

        RemotePlayer remotePlayer = _remotePlayers.Find((RemotePlayer rp) => rp.Id == serverMessageLeftGame.id);
        if (remotePlayer != null) {
            _remotePlayers.Remove(remotePlayer);
            Destroy(remotePlayer.gameObject);
        }
    }

    private void OnServerMessageAttack(ServerMessageAttack messageAttack) {
        if (messageAttack.id == _myPlayer.Id)
            return;

        RemotePlayer remotePlayer = _remotePlayers.Find((RemotePlayer rp) => rp.Id == messageAttack.id);
        if (remotePlayer != null) {
            remotePlayer.Attack.Attack();
        }
    }

    private void Start() {
        MessageHandler.Current.onServerMessageGameState += OnServerMessageGameState;
        MessageHandler.Current.onServerMessageJoinedGame += OnServerMessageJoinedGame;
        MessageHandler.Current.onServerMessageMovements += OnServerMessageMovements;
        MessageHandler.Current.onServerMessageLeftGame += OnServerMessageLeftGame;
        MessageHandler.Current.onServerMessageAttack += OnServerMessageAttack;
        TCPClient.Send(new ClientMessagePlay());
    }
}