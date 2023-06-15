using System.Timers;

namespace TestUDP;

public class Players
{
    private List<Player> _players = new List<Player>();

    private void Sync() {
        if (_players.Count < 2)
            return;
        BroadcastUDP(new ServerMessageMovements(GetObjectDatas().ToArray()));
    }

    public void StartSyncing() {
        System.Timers.Timer timer = new System.Timers.Timer(1000 / Config.SyncFrequency);
        timer.Elapsed += (object sender, ElapsedEventArgs e) => { Sync(); };
        timer.AutoReset = true;
        timer.Start();
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
        _players.Add(newPlayer);
        Console.WriteLine($"[Players] {_players.Count}");
        return newPlayer;
    }

    public Player? Find(Client client) {
        return _players.Find((Player p) => p.Client == client);
    }

    public Player? Find(TCPClient tcp) {
        return _players.Find((Player p) => p.Client.Tcp == tcp);
    }

    public bool Any(TCPClient tcp) {
        return _players.Any((Player p) => p.Client.Tcp == tcp);
    }

    public void Remove(TCPClient tcp) {
        int index = _players.FindIndex((Player p) => p.Client.Tcp == tcp);
        if (index != -1)
            _players.RemoveAt(index);
        Console.WriteLine($"[Players] {_players.Count}");
    }

    public List<ObjectData> GetObjectDatas() {
        return _players.Select((Player player) => player.Data).ToList();
    }

    public List<Client> GetClients() {
        return _players.Select((Player player) => player.Client).ToList();
    }
}
