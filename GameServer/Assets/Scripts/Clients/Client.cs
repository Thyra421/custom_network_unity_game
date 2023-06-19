using UnityEngine;

public class Client
{
    private readonly TCPClient _tcp;
    private bool _authenticated = false;
    private UDPClient _udp;
    private string _secret;
    private Player _player;

    public Client(TCPClient tcp) {
        _tcp = tcp;
        tcp.Client = this;
        Debug.Log($"[Client] created");
    }

    public void Authenticate(UDPClient udp, string secret) {
        _udp = udp;
        _secret = secret;
        _authenticated = true;
    }

    public TCPClient Tcp => _tcp;

    public UDPClient Udp => _udp;

    public string Secret => _secret;

    public bool Authenticated => _authenticated;

    public Player Player {
        get => _player;
        set => _player = value;
    }
}

