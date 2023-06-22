using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    private const int MAX_PLAYERS = 2;
    private readonly List<Player> _players = new List<Player>();
    private readonly List<Node> _nodes = new List<Node>();
    private float _elapsedTime = 0f;

    private delegate T CustomMessageHandler<T>(Player player);
    private delegate bool SendConditionHandler<T>(T message);

    private void SyncMovement() {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= (1f / SharedConfig.SYNC_FREQUENCY)) {
            _elapsedTime = 0f;
            if (_players.Count < 2)
                return;
            PlayerData[] playerDatas = GetPlayerDatas((Player p) => p.UpdateTransformIfChanged());
            if (playerDatas.Length > 0)
                BroadcastUDP((Player player) => new MessageMoved(Array.FindAll(playerDatas, (PlayerData data) => data.id != player.Id).ToArray()), (MessageMoved message) => message.players.Length > 0);
        }
    }

    private PlayerData[] GetPlayerDatas(Predicate<Player> condition) =>
        _players.FindAll(condition).Select((Player player) => player.Data).ToArray();

    private Client[] Clients => _players.Select((Player player) => player.Client).ToArray();

    private void BroadcastUDP<T>(CustomMessageHandler<T> customMessage, SendConditionHandler<T> condition) {
        foreach (Client client in Clients) {
            T message = customMessage(client.Player);
            if (condition(message))
                client.Udp?.Send(message);
        }
    }

    private void PrepareBiome(DropSource[] dropSources, int amount, List<Transform> occupied) {
        for (int i = 0; i < amount; i++) {
            Transform randomTransform = GameManager.Current.RandomPlainSpawn;
            while (occupied.Contains(randomTransform))
                randomTransform = GameManager.Current.RandomPlainSpawn;
            occupied.Add(randomTransform);

            DropSource dropSource = dropSources[UnityEngine.Random.Range(0, dropSources.Length)];

            GameObject newObject = Instantiate(Resources.Load<GameObject>("Shared/Prefabs/" + dropSource.Prefab.name), randomTransform.position, randomTransform.rotation, transform);
            Node newNode = newObject.AddComponent<Node>();
            newNode.GenerateDrops(dropSource);
            _nodes.Add(newNode);
        }
    }

    private void PrepareGame() {
        List<Transform> occupied = new List<Transform>();

        PrepareBiome(Resources.LoadAll<DropSource>("Shared/DropSources/Plains/Common"), 20, occupied);
        PrepareBiome(Resources.LoadAll<DropSource>("Shared/DropSources/Plains/Rare"), 5, occupied);
    }

    private void Awake() {
        PrepareGame();
    }

    private void Update() {
        SyncMovement();
    }

    public async void BroadcastTCP<T>(T message) {
        foreach (Client client in Clients) {
            await client.Tcp.Send(message);
        }
    }

    public async void BroadcastTCP<T>(T message, Player except) {
        foreach (Client client in Clients) {
            if (client != except.Client)
                await client.Tcp.Send(message);
        }
    }

    public Player CreatePlayer(Client client) {
        GameObject newGameObject = Instantiate(Resources.Load<GameObject>("Prefabs/Player"), transform);
        Player newPlayer = newGameObject.GetComponent<Player>();
        newGameObject.name = "Player " + newPlayer.Id;
        newPlayer.Initialize(client, this);
        _players.Add(newPlayer);
        Debug.Log($"[Players] created. {_players.Count} players");
        return newPlayer;
    }

    public void RemovePlayer(Player player) {
        Destroy(player.gameObject);
        _players.Remove(player);
        Debug.Log($"[Players] removed. {_players.Count} players");
        if (_players.Count == 0)
            Reception.Current.RemoveRoom(this);
    }

    public void RemoveNode(Node node) {
        Destroy(node.gameObject);
        _nodes.Remove(node);
    }

    public Node FindNode(string id) {
        return _nodes.Find((Node m) => m.Id == id);
    }

    public PlayerData[] PlayerDatas => _players.Select((Player player) => player.Data).ToArray();

    public NodeData[] NodeDatas => _nodes.Select((Node node) => node.Data).ToArray();

    public bool IsFull => _players.Count == MAX_PLAYERS;

    public List<Node> Nodes => _nodes;
}
