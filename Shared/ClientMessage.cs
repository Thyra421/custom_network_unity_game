public struct MessageLogin
{
    public string username;

    public MessageLogin(string username) {
        this.username = username;
    }
}

public struct MessageAuthenticate
{
    public string secret;
    public string udpAddress;
    public int udpPort;

    public MessageAuthenticate(string secret, string udpAddress, int udpPort) {
        this.secret = secret;
        this.udpAddress = udpAddress;
        this.udpPort = udpPort;
    }
}

public struct MessagePlay
{
}

public struct MessageMovement
{
    public TransformData newTransform;
    public MovementData movement;

    public MessageMovement(TransformData newTransform, MovementData movement) {
        this.newTransform = newTransform;
        this.movement = movement;
    }
}

public struct MessageAttack
{
}