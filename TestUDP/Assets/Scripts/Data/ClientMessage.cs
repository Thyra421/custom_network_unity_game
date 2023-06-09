public enum ClientMessageType
{
    authenticate, position
}

public abstract class ClientMessage
{
    public string id = NetworkManager.current.GetId;
    public ClientMessageType type;

    protected ClientMessage(ClientMessageType type) {
        this.type = type;
    }
}

public class ClientMessagePosition : ClientMessage
{
    public Vector3Data position;

    public ClientMessagePosition(Vector3Data position) : base(ClientMessageType.position) {
        this.position = position;
    }
}

public class ClientMessageAuthenticate : ClientMessage
{
    public ClientMessageAuthenticate() : base(ClientMessageType.authenticate) { }
}