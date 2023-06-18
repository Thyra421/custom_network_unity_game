public struct MessageSecret
{
    public string secret;

    public MessageSecret(string secret) {
        this.secret = secret;
    }
}

public struct MessageJoinedGame
{
    public ObjectData player;

    public MessageJoinedGame(ObjectData player) {
        this.player = player;
    }
}

public struct MessageMovements
{
    public ObjectData[] players;

    public MessageMovements(ObjectData[] players) {
        this.players = players;
    }
}

public struct MessageGameState
{
    public string id;
    public ObjectData[] players;

    public MessageGameState(string id, ObjectData[] players) {
        this.id = id;
        this.players = players;
    }
}

public struct MessageLeftGame
{
    public string id;

    public MessageLeftGame(string id) {
        this.id = id;
    }
}

public struct MessagePlayerAttack
{
    public string id;

    public MessagePlayerAttack(string id) {
        this.id = id;
    }
}

public struct MessageDamage
{
    public string idFrom;
    public string idTo;

    public MessageDamage(string idFrom, string idTo) {
        this.idFrom = idFrom;
        this.idTo = idTo;
    }
}
