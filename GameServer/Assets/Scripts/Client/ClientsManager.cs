using System.Collections.Generic;
using UnityEngine;

public class ClientsManager
{
    public List<Client> Clients { get; } = new List<Client>();

    public Client Create(TCPClient tcpClient) {
        Client newClient = new Client(tcpClient);
        Clients.Add(newClient);
        Debug.Log($"[ClientsManager] created => {Clients.Count} clients");
        return newClient;
    }

    public void Remove(Client client) {
        Clients.Remove(client);
        Debug.Log($"[ClientsManager] removed => {Clients.Count} clients");
    }

    public Client Find(string address, int port) {
        return Clients.Find((Client c) => c.UDP?.Address == address && c.UDP.Port == port);
    }
}
