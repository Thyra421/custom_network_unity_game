using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class PlayersManager : MonoBehaviour
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
            PlayerData[] playerDatas = GetPlayerDatas((Player p) => p.Movement.UpdateTransformIfChanged());
            if (playerDatas.Length > 0)
                BroadcastUDP((Player player) => new MessagePlayerMoved(Array.FindAll(playerDatas, (PlayerData data) => data.id != player.Id).ToArray()), (MessagePlayerMoved message) => message.players.Length > 0);
        }
    }

    private void BroadcastUDP<T>(CustomMessageHandler<T> customMessage, SendConditionHandler<T> condition) {
        foreach (Client client in Clients) {
            T message = customMessage(client.Player);
            if (condition(message))
                client.Udp?.Send(message);
        }
    }

    private PlayerData[] GetPlayerDatas(Predicate<Player> condition) =>
        _players.FindAll(condition).Select((Player player) => player.Data).ToArray();

    private Client[] Clients => _players.Select((Player player) => player.Client).ToArray();

    private void Update() {
        SyncMovement();
    }

    public void BroadcastUDP<T>(T message) {
        foreach (Client client in Clients) {
            client.Udp?.Send(message);
        }
    }

    public void BroadcastTCP<T>(T message) {
        foreach (Client client in Clients)
            client.Tcp.Send(message);
    }

    public void BroadcastTCP<T>(T message, Player except) {
        foreach (Client client in Clients)
            if (client != except.Client)
                client.Tcp.Send(message);
    }

    public Player CreatePlayer(Client client) {
        GameObject newGameObject = Instantiate(GameManager.Current.PlayerTemplate, transform);
        Player newPlayer = newGameObject.GetComponent<Player>();
        newGameObject.name = "Player " + newPlayer.Id;
        newPlayer.Initialize(client, _room);
        _players.Add(newPlayer);
        BroadcastTCP(new MessageJoinedGame(newPlayer.Data), newPlayer);
        Debug.Log($"[Players] created => {_players.Count} players");
        return newPlayer;
    }

    public void RemovePlayer(Player player) {
        BroadcastTCP(new MessageLeftGame(player.Id), player);
        _players.Remove(player);
        Destroy(player.gameObject);
        Debug.Log($"[Players] removed => {_players.Count} players");
        if (_players.Count == 0)
            Reception.Current.RemoveRoom(_room);
    }

    public PlayerData[] PlayerDatas => _players.Select((Player player) => player.Data).ToArray();

    public bool IsFull => _players.Count == MAX_PLAYERS;
}