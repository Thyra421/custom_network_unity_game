using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private GameObject _nodeGUITemplate;
    [SerializeField]
    private LocalPlayer _myPlayer;
    private readonly List<RemotePlayer> _remotePlayers = new List<RemotePlayer>();
    private readonly List<Node> _nodes = new List<Node>();

    private Node FindNode(string id) => _nodes.Find((Node n) => n.Id == id);

    private RemotePlayer FindPlayer(string id) => _remotePlayers.Find((RemotePlayer p) => p.Id == id);

    private void CreatePlayer(PlayerData data) {
        GameObject newPlayer = Instantiate(_playerPrefab, data.transform.position.ToVector3, Quaternion.identity);
        RemotePlayer remotePlayer = newPlayer.GetComponent<RemotePlayer>();
        remotePlayer.Id = data.id;
        _remotePlayers.Add(remotePlayer);
    }

    private void CreateNode(NodeData data) {
        GameObject newObject = Instantiate(Resources.Load<GameObject>("Shared/Prefabs/" + data.assetName), data.transform.position.ToVector3, Quaternion.Euler(data.transform.rotation.ToVector3));
        NodeGUI nodeGUI = Instantiate(_nodeGUITemplate, newObject.transform).GetComponent<NodeGUI>();
        Node newNode = newObject.AddComponent<Node>();

        newNode.Id = data.id;
        newNode.RemainingLoots = data.remainingLoots;
        newNode.onChanged += nodeGUI.OnChanged;
        nodeGUI.Slider.maxValue = data.remainingLoots;
        nodeGUI.Slider.value = data.remainingLoots;
        _nodes.Add(newNode);
    }

    private void OnMessageGameState(MessageGameState messageGameState) {
        _myPlayer.Id = messageGameState.id;
        foreach (PlayerData p in messageGameState.players) {
            if (p.id != _myPlayer.Id)
                CreatePlayer(p);
        }
    }

    private void OnMessageSpawnNodes(MessageSpawnNodes messageSpawnNodes) {
        foreach (NodeData o in messageSpawnNodes.nodes) {
            CreateNode(o);
        }
    }

    private void OnMessageLooted(MessageLooted messageLooted) {
        FindNode(messageLooted.id).RemoveLoot();
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
        if (remotePlayer != null) {
            _remotePlayers.Remove(remotePlayer);
            Destroy(remotePlayer.gameObject);
        }
    }

    private void OnMessageAttacked(MessageAttacked messageAttacked) {
        if (messageAttacked.id == _myPlayer.Id)
            return;

        RemotePlayer remotePlayer = FindPlayer(messageAttacked.id);
        if (remotePlayer != null) {
            remotePlayer.Attack.Attack();
        }
    }

    private void OnMessageDamage(MessageDamage messageDamage) {
        if (messageDamage.idTo == _myPlayer.Id)
            _myPlayer.Health.TakeDamage(10);
        else {
            RemotePlayer remotePlayer = FindPlayer(messageDamage.idTo);
            if (remotePlayer != null) {
                remotePlayer.Health.TakeDamage(10);
            }
        }
    }

    private void OnMessageDespawnObject(MessageDespawnObject messageDespawnObject) {
        Node node = FindNode(messageDespawnObject.id);
        _nodes.Remove(node);
        Destroy(node.gameObject);
    }

    private void OnMessageInventoryAdd(MessageInventoryAdd messageInventoryAdd) {
        Item item = Resources.Load<Item>("Shared/RawMaterials/" + messageInventoryAdd.itemName);
        _myPlayer.Inventory.Add(item, messageInventoryAdd.amount);
    }

    private void Awake() {
        MessageHandler.onMessageGameState = OnMessageGameState;
        MessageHandler.onMessageJoinedGame += OnMessageJoinedGame;
        MessageHandler.onMessageMoved += OnMessageMoved;
        MessageHandler.onMessageLeftGame += OnMessageLeftGame;
        MessageHandler.onMessageAttacked += OnMessageAttacked;
        MessageHandler.onMessageDamage += OnMessageDamage;
        MessageHandler.onMessageDespawnObject += OnMessageDespawnObject;
        MessageHandler.onMessageSpawnNodes += OnMessageSpawnNodes;
        MessageHandler.onMessageInventoryAdd += OnMessageInventoryAdd;
        MessageHandler.onMessageLooted += OnMessageLooted;
    }

    private void Start() {
        TCPClient.Send(new MessagePlay());
    }

    public LocalPlayer MyPlayer => _myPlayer;
}