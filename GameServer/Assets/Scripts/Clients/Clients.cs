using System.Collections.Generic;
using UnityEngine;

public class Clients
{
    private readonly List<Client> _clients = new List<Client>();

    public Client Create(TCPClient tcpClient) {
        Client newClient = new Client(tcpClient);
        _clients.Add(newClient);
        Debug.Log($"[Clients] created. {_clients.Count} clients");
        return newClient;
    }

    public void Remove(Client client) {
        _clients.Remove(client);
        Debug.Log($"[Clients] removed. {_clients.Count} clients");
    }

    public void Remove(TCPClient tcpClient) {
        Client client = _clients.Find((Client c) => c.Tcp == tcpClient);
        if (client != null)
            Remove(client);
    }

    public Client Find(string address, int port) {
        return _clients.Find((Client c) => c.Udp?.Address == address && c.Udp.Port == port);
    }

    public Client Find(TCPClient tcpClient) {
        return _clients.Find((Client c) => c.Tcp == tcpClient);
    }

    public Client Find(string secret) {
        return _clients.Find((Client c) => c.Secret == secret);
    }
}
