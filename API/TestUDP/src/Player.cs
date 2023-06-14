namespace TestUDP;

public class Player
{
    private readonly Client _client;
    private ObjectData _data;

    public Player(Client client) {
        _data = new ObjectData();
        _client = client;
    }

    public Client Client => _client;

    public ObjectData Data => _data;
}
