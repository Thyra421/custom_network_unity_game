using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class RoomPlayersManager : MonoBehaviour
{
    private const int MAX_PLAYERS = 2;
    [SerializeField]
    private Room _room;
    private readonly List<Player> _players = new List<Player>();
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

    private void Update() {
        SyncMovement();
    }

    public async void BroadcastTCP<T>(T message) {
        foreach (Client client in Clients)
            await client.Tcp.Send(message);
    }

    public async void BroadcastTCP<T>(T message, Player except) {
        foreach (Client client in Clients)
            if (client != except.Client)
                await client.Tcp.Send(message);
    }

    public Player CreatePlayer(Client client) {
        GameObject newGameObject = Instantiate(Resources.Load<GameObject>($"{Config.PREFABS_PATH}/Player"), transform);
        Player newPlayer = newGameObject.GetComponent<Player>();
        newGameObject.name = "Player " + newPlayer.Id;
        newPlayer.Initialize(client, _room);
        _players.Add(newPlayer);
        Debug.Log($"[Players] created => {_players.Count} players");
        return newPlayer;
    }

    public void RemovePlayer(Player player) {
        _players.Remove(player);
        Destroy(player.gameObject);
        Debug.Log($"[Players] removed => {_players.Count} players");
        if (_players.Count == 0)
            Reception.Current.RemoveRoom(_room);
    }

    public PlayerData[] PlayerDatas => _players.Select((Player player) => player.Data).ToArray();

    public bool IsFull => _players.Count == MAX_PLAYERS;
}