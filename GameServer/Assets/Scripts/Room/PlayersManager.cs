using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class PlayersManager : MonoBehaviour
{
    [SerializeField]
    private Room _room;
    private readonly List<Player> _players = new List<Player>();
    private float _elapsedTime = 0f;

    private delegate T CustomMessageHandler<T>(Player player);
    private delegate bool SendConditionHandler<T>(T message);

    private Client[] Clients => _players.Select((Player player) => player.Client).ToArray();

    public PlayerData[] Datas => _players.Select((Player player) => player.Data).ToArray();
    public bool IsFull => _players.Count == Config.Current.MaxPlayersPerRoom;

    private PlayerMovementData[] FindAllMovementDatas(Predicate<Player> condition) => _players.FindAll(condition).Select((Player player) => player.Movement.Data).ToArray();

    private void SyncMovement() {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= (1f / SharedConfig.Current.SyncFrequency)) {
            _elapsedTime = 0f;

            if (_players.Count < 2)
                return;

            PlayerMovementData[] playerMovementDatas = FindAllMovementDatas((Player p) => p.UpdateTransformIfChanged());

            if (playerMovementDatas.Length > 0)
                BroadcastUDP((Player player) => new MessagePlayersMoved(Array.FindAll(playerMovementDatas, (PlayerMovementData data) => data.id != player.Id).ToArray()), (MessagePlayersMoved message) => message.players.Length > 0);
        }
    }

    private void BroadcastUDP<T>(CustomMessageHandler<T> customMessage, SendConditionHandler<T> condition) {
        foreach (Client client in Clients) {
            T message = customMessage(client.Player);

            if (condition(message))
                client.UDP?.Send(message);
        }
    }

    private void Update() {
        SyncMovement();
    }

    public void BroadcastUDP<T>(T message) {
        foreach (Client client in Clients) {
            client.UDP?.Send(message);
        }
    }

    public void BroadcastTCP<T>(T message) {
        foreach (Client client in Clients)
            client.TCP.Send(message);
    }

    public void BroadcastTCP<T>(T message, Player except) {
        foreach (Client client in Clients)
            if (client != except.Client)
                client.TCP.Send(message);
    }

    public Player CreatePlayer(Client client) {
        GameObject newGameObject = Instantiate(GameManager.Current.PlayerTemplate, transform);
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