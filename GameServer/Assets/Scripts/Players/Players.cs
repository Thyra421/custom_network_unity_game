using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Players : IUpdatable
{
    private List<Player> _players = new List<Player>();

    private void SyncMovement() {
        if (_players.Count < 2)
            return;
        ObjectData[] objects = GetObjectDatas((Player p) => p.UpdateIfHasChanged());
        if (objects.Length > 0)
            BroadcastUDP(new ServerMessageMovements(objects));
    }

    public void Update() {
        SyncMovement();
    }

    public async void BroadcastTCP(ServerMessage message) {
        foreach (Client client in GetClients()) {
            await client.Tcp.Send(message);
        }
    }

    public async void BroadcastTCP(ServerMessage message, Player except) {
        foreach (Client client in GetClients()) {
            if (client != except.Client)
                await client.Tcp.Send(message);
        }
    }

    public void BroadcastUDP(ServerMessage message) {
        foreach (Client client in GetClients()) {
            client.Udp?.Send(message);
        }
    }

    public Player Create(Client client) {
        Player newPlayer = new Player(client);
        client.Player = newPlayer;
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

    public bool Any(TCPClient tcp) {
        return _players.Any((Player p) => p.Client.Tcp == tcp);
    }

    public bool Remove(TCPClient tcp) {
        Player player = _players.Find((Player p) => p.Client.Tcp == tcp);
        if (player == null)
            return false;
        return Remove(player);
    }

    public bool Remove(Player player) {
        if (player == null)
            return false;
        GameManager.Current.DestroyAvatar(player.Avatar);
        bool result = _players.Remove(player);
        if (result)
            Debug.Log($"[Players] removed. {_players.Count} players");
        return result;
    }

    public ObjectData[] GetObjectDatas(Predicate<Player> condition) {
        return _players.FindAll(condition).Select((Player player) => player.Data).ToArray();
    }

    public ObjectData[] GetObjectDatas() {
        return _players.Select((Player player) => player.Data).ToArray();
    }

    public Client[] GetClients() {
        return _players.Select((Player player) => player.Client).ToArray();
    }
}
