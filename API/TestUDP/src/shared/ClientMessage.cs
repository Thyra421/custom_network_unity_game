namespace TestUDP;

public enum ClientMessageType
{
    login, // HTTP
    authenticate, // TCP
    play, // HTTP
    position // UDP
}

public class ClientMessage
{
    public ClientMessageType type;

    public ClientMessage(ClientMessageType type) {
        this.type = type;
    }
}

public class ClientMessageLogin : ClientMessage
{
    public string username;

    public ClientMessageLogin(string username) : base(ClientMessageType.login) {
        this.username = username;
    }
}

public class ClientMessageAuthenticate : ClientMessage
{
    public string secret;
    public string udpAddress;
    public int udpPort;

    public ClientMessageAuthenticate(string secret, string udpAddress, int udpPort) : base(ClientMessageType.authenticate) {
        this.secret = secret;
        this.udpAddress = udpAddress;
        this.udpPort = udpPort;
    }
}

public class ClientMessagePlay : ClientMessage
{
    public string secret;

    public ClientMessagePlay(string secret) : base(ClientMessageType.play) {
        this.secret = secret;
    }
}

public class ClientMessagePosition : ClientMessage
{
    public Vector3Data position;

    public ClientMessagePosition(Vector3Data position) : base(ClientMessageType.position) {
        this.position = position;
    }
}