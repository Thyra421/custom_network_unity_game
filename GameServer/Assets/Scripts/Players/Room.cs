using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    private const int _maxPlayers = 2;
    private readonly List<Player> _players = new List<Player>();
    private readonly List<Mushroom> _mushrooms = new List<Mushroom>();
    private float _elapsedTime = 0f;

    private delegate T CustomMessageHandler<T>(Player player);
    private delegate bool SendConditionHandler<T>(T message);

    private void SyncMovement() {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= (1f / Config.SyncFrequency)) {
            _elapsedTime = 0f;
            if (_players.Count < 2)
                return;
            PlayerData[] playerDatas = GetPlayerDatas((Player p) => p.UpdateTransformIfChanged());
            if (playerDatas.Length > 0)
                BroadcastUDP((Player player) => new MessageMovements(Array.FindAll(playerDatas, (PlayerData data) => data.id != player.Id).ToArray()), (MessageMovements message) => message.players.Length > 0);
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

    private void PrepareGame() {
        List<Transform> spawned = new List<Transform>();

        for (int i = 0; i < 5; i++) {
            Transform randomTransform = GameManager.Current.RandomMusroomSpawn;
            while (spawned.Contains(randomTransform))
                randomTransform = GameManager.Current.RandomMusroomSpawn;
            spawned.Add(randomTransform);
            Mushroom newMushroom = Instantiate(Resources.Load("Prefabs/Mushroom"), randomTransform.position, randomTransform.rotation, transform).GetComponent<Mushroom>();
            _mushrooms.Add(newMushroom);
        }
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

    public void RemoveMushroom(Mushroom mushroom) {
        Destroy(mushroom.gameObject);
        _mushrooms.Remove(mushroom);
    }

    public Mushroom FindMushroom(string id) {
        return _mushrooms.Find((Mushroom m) => m.Id == id);
    }

    public PlayerData[] PlayerDatas => _players.Select((Player player) => player.Data).ToArray();

    public ObjectData[] ObjectDatas => _mushrooms.Select((Mushroom mushroom) => mushroom.Data).ToArray();

    public bool IsFull => _players.Count == _maxPlayers;

    public List<Mushroom> Mushrooms => _mushrooms;
}
