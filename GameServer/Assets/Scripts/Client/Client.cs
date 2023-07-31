public class Client
{
    public TCPClient TCP { get; private set; }
    public UDPClient UDP { get; private set; }
    public string Secret { get; private set; }
    public Player Player { get; set; }
    public bool Authenticated => UDP != null && UDP!= null;

    public Client(TCPClient tcp) {
        TCP = tcp;
        tcp.Client = this;
    }

    public void Authenticate(UDPClient udp, string secret) {
        UDP = udp;
        Secret = secret;
    }
}

