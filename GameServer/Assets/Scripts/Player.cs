public class Player
{
    private readonly Client _client;
    private ObjectData _data;
    private TransformData _lastTransform;

    public Player(Client client) {
        _data = new ObjectData();
        _client = client;
    }

    public bool UpdateIfHasChanged() {
        if (_lastTransform?.Equals(Data.transform) ?? false)
            return false;
        else {
            _lastTransform = Data.transform;
            return true;
        }
    }

    public Client Client => _client;

    public ObjectData Data => _data;

    public TransformData LastTransform {
        get => _lastTransform;
        set => _lastTransform = value;
    }
}
