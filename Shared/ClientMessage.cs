public enum ClientMessageType
{
    login, // HTTP
    authenticate, // TCP
    play, // HTTP
    movement, // UDP
    attack // TCP
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
    public ClientMessagePlay() : base(ClientMessageType.play) {
    }
}

public class ClientMessageMovement : ClientMessage
{
    public TransformData newTransform;
    public MovementData movement;

    public ClientMessageMovement(TransformData newTransform, MovementData movement) : base(ClientMessageType.movement) {
        this.newTransform = newTransform;
        this.movement = movement;
    }
}

public class ClientMessageAttack : ClientMessage
{
    public ClientMessageAttack() : base(ClientMessageType.attack) {
    }
}