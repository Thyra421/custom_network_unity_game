using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class PlayersManager : MonoBehaviour
{
    [SerializeField]
    private Room _room;
    [SerializeField]
    private GameObject _playerPrefab;
    private readonly List<Player> _players = new List<Player>();
    private float _elapsedTime = 0f;

    private Client[] Clients => _players.Select((Player player) => player.Client).ToArray();

    public PlayerData[] Datas => _players.Select((Player player) => player.Data).ToArray();
    public bool IsFull => _players.Count == Config.Current.MaxPlayersPerRoom;

    private PlayerMovementData[] FindAllMovementDatas(Predicate<Player> condition) => _players.FindAll(condition).Select((Player player) => player.Movement.Data).ToArray();

    private void SyncMovement() {
        if (_players.Count < 2)
            return;

        PlayerMovementData[] playerMovementDatas = FindAllMovementDatas((Player p) => p.UpdateTransformIfChanged());

        if (playerMovementDatas.Length > 0)
            BroadcastUDP(new MessagePlayersMoved(playerMovementDatas));
    }

    private void Update() {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= (1f / SharedConfig.Current.SyncFrequency)) {
            _elapsedTime = 0f;

            SyncMovement();
        }
    }

    public void BroadcastUDP<T>(T message) {
        byte[] bytes = Utils.GetBytes(message);

        foreach (Client client in Clients) {
            client.UDP.SendBytes(bytes);
        }
    }

    public void BroadcastTCP<T>(T message) {
        byte[] bytes = Utils.GetBytesForTCP(message);

        foreach (Client client in Clients)
            client.TCP.SendBytes(bytes);
    }

    public void BroadcastTCP<T>(T message, Player except) {
        byte[] bytes = Utils.GetBytesForTCP(message);

        foreach (Client client in Clients)
            if (client != except.Client)
                client.TCP.SendBytes(bytes);
    }

    public Player CreatePlayer(Client client) {
        GameObject newGameObject = Instantiate(_playerPrefab, transform);
        Player newPlayer = newGameObject.GetComponent<Player>();
        newGameObject.name = "Player " + newPlayer.Id;
        newPlayer.Initialize(_room, client);
        client.Player = newPlayer;
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
}