using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    private int _maxPlayers = 2;
    private List<Player> _players = new List<Player>();
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

    public Player Find(Client client) {
        return _players.Find((Player p) => p.Client == client);
    }

    public Player Find(TCPClient tcp) {
        return _players.Find((Player p) => p.Client.Tcp == tcp);
    }

    public Player Find(string address, int port) {
        return _players.Find((Player p) => p.Client.Udp?.Address == address && p.Client.Udp.Port == port);
    }

    public void Remove(TCPClient tcp) {
        Player player = _players.Find((Player p) => p.Client.Tcp == tcp);
        Remove(player);
    }

    public void Remove(Player player) {
        Destroy(player.gameObject);
        _players.Remove(player);
        Debug.Log($"[Players] removed. {_players.Count} players");
        if (_players.Count == 0)
            Reception.Current.Remove(this);
    }

    public PlayerData[] PlayerDatas => _players.Select((Player player) => player.Data).ToArray();

    public bool IsFull => _players.Count == _maxPlayers;
}
