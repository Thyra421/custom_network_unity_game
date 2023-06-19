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
    public AnimationData movement;

    public MessageMovement(TransformData newTransform, AnimationData movement) {
        this.newTransform = newTransform;
        this.movement = movement;
    }
}

public struct MessageAttack
{
}

public struct MessagePickUp
{
    public string id;

    public MessagePickUp(string id) {
        this.id = id;
    }
}