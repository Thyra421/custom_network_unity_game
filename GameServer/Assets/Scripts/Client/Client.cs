public class Client
{
    public TCPClient TCP { get; private set; }
    public UDPClient UDP { get; private set; }
    public string Secret { get; private set; }
    public Player Player { get; set; }
    public bool Authenticated => UDP != null && UDP != null;    

    private void Authenticate(UDPClient udp, string secret) {
        UDP = udp;
        Secret = secret;
    }

    private void OnMessagePlay(MessagePlay messagePlay) {
        Reception.Current.JoinOrCreateRoom(this);
    }

    private void OnMessageAuthenticate(MessageAuthenticate messageAuthenticate) {
        Authenticate(new UDPClient(messageAuthenticate.udpAddress, messageAuthenticate.udpPort), messageAuthenticate.secret);
    }

    public Client(TCPClient tcp) {
        TCP = tcp;
        tcp.Client = this;
        TCP.MessageHandler.AddListener<MessageAuthenticate>(OnMessageAuthenticate);
        TCP.MessageHandler.AddListener<MessagePlay>(OnMessagePlay);
    }
}

