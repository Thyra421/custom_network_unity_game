namespace TestUDP;

public class Clients
{
    private readonly List<Client> _clients = new List<Client>();

    public Client Create(TCPClient tcpClient) {
        Client newClient = new Client(tcpClient);
        _clients.Add(newClient);
        Console.WriteLine($"[Clients] {_clients.Count}");
        return newClient;
    }

    public void Remove(Client client) {
        _clients.Remove(client);
        Console.WriteLine($"[Clients] {_clients.Count}");
    }

    public void Remove(TCPClient tcpClient) {
        int index = _clients.FindIndex((Client c) => { return c.Tcp == tcpClient; });
        if (index != -1)
            _clients.RemoveAt(index);
        Console.WriteLine($"[Clients] {_clients.Count}");
    }

    public Client? Find(string address, int port) {
        return _clients.Find((Client c) => c.Udp?.Address == address && c.Udp.Port == port);
    }

    public Client? Find(TCPClient tcpClient) {
        return _clients.Find((Client c) => c.Tcp == tcpClient);
    }

    //public Client Find(string id) {
    //    return _clients.Find((Client c) => c.Id == id);
    //}

    public Client? Find(string secret) {
        return _clients.Find((Client c) => c.Secret == secret);
    }
}
