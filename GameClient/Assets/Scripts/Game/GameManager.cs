using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private GameObject _nodePrefab;
    [SerializeField]
    private LocalPlayer _myPlayer;
    private readonly List<RemotePlayer> _remotePlayers = new List<RemotePlayer>();
    private readonly List<Node> _nodes = new List<Node>();

    private void CreatePlayer(PlayerData data) {
        GameObject newPlayer = Instantiate(_playerPrefab, data.transform.position.ToVector3, Quaternion.identity);
        RemotePlayer remotePlayer = newPlayer.GetComponent<RemotePlayer>();
        remotePlayer.Id = data.id;
        _remotePlayers.Add(remotePlayer);
    }

    private void CreateNode(ObjectData data) {
        Node newNode = Instantiate(_nodePrefab, data.transform.position.ToVector3, Quaternion.Euler(data.transform.rotation.ToVector3)).GetComponent<Node>();
        Instantiate(Resources.Load("Shared/Prefabs/" + data.assetName), newNode.transform);
        newNode.Id = data.id;
        _nodes.Add(newNode);
    }

    private void OnMessageGameState(MessageGameState messageGameState) {
        _myPlayer.Id = messageGameState.id;
        foreach (PlayerData p in messageGameState.players) {
            if (p.id != _myPlayer.Id)
                CreatePlayer(p);
        }
        foreach (ObjectData o in messageGameState.nodes) {
            CreateNode(o);
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

    private void OnMessagePickedUp(MessagePickedUp messagePickedUp) {
        Node node = _nodes.Find((Node m) => m.Id == messagePickedUp.objectId);
        _nodes.Remove(node);
        Destroy(node.gameObject);
    }

    private void Start() {
        MessageHandler.Current.onMessageGameState += OnMessageGameState;
        MessageHandler.Current.onMessageJoinedGame += OnMessageJoinedGame;
        MessageHandler.Current.onMessageMovements += OnMessageMovements;
        MessageHandler.Current.onMessageLeftGame += OnMessageLeftGame;
        MessageHandler.Current.onMessageAttacked += OnMessageAttacked;
        MessageHandler.Current.onMessageDamage += OnMessageDamage;
        MessageHandler.Current.onMessagePickedUp += OnMessagePickedUp;
        TCPClient.Send(new MessagePlay());
    }

    public LocalPlayer MyPlayer => _myPlayer;
}