using System.Collections.Generic;
using UnityEngine;

public class Clients
{
    private readonly List<Client> _clients = new List<Client>();

    public Client Create(TCPClient tcpClient) {
        Client newClient = new Client(tcpClient);
        tcpClient.Client = newClient;
        _clients.Add(newClient);
        Debug.Log($"[Clients] created. {_clients.Count} clients");
        return newClient;
    }

    public void Remove(Client client) {
        _clients.Remove(client);
        Debug.Log($"[Clients] removed. {_clients.Count} clients");
    }

    public void Remove(TCPClient tcpClient) {
        int index = _clients.FindIndex((Client c) => { return c.Tcp == tcpClient; });
        if (index != -1)
            _clients.RemoveAt(index);
        Debug.Log($"[Clients] removed. {_clients.Count} clients");
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
