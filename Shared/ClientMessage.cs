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
    public AnimationData animation;

    public MessageMovement(TransformData newTransform, AnimationData animation) {
        this.newTransform = newTransform;
        this.animation = animation;
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

public struct MessageCraft
{
    public string patternName;

    public MessageCraft(string patternName) {
        this.patternName = patternName;
    }
}