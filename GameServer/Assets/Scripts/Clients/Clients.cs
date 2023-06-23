﻿using System.Collections.Generic;
using UnityEngine;

public class Clients
{
    private readonly List<Client> _clients = new List<Client>();

    public Client Create(TCPClient tcpClient) {
        Client newClient = new Client(tcpClient);
        _clients.Add(newClient);
        Debug.Log($"[Clients] created => {_clients.Count} clients");
        return newClient;
    }

    public void Remove(Client client) {
        _clients.Remove(client);
        Debug.Log($"[Clients] removed => {_clients.Count} clients");
    }

    public Client Find(string address, int port) {
        return _clients.Find((Client c) => c.Udp?.Address == address && c.Udp.Port == port);
    }
}
