public class Client
{
    private readonly TCPClient _tcp;
    private UDPClient _udp;
    private string _secret;
    private Player _player;

    public Client(TCPClient tcp) {
        _tcp = tcp;
        tcp.Client = this;
    }

    public void Authenticate(UDPClient udp, string secret) {
        _udp = udp;
        _secret = secret;
    }

    public TCPClient Tcp => _tcp;

    public UDPClient Udp => _udp;

    public string Secret => _secret;

    public bool Authenticated => _udp != null && _secret != null;

    public Player Player {
        get => _player;
        set => _player = value;
    }
}

