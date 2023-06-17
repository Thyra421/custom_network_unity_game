public enum ServerMessageType
{
    secret, // HTTP
    gameState, // TCP
    joinedGame, // TCP
    leftGame, // TCP
    movements // UDP
}

public class ServerMessage
{
    public ServerMessageType type;

    public ServerMessage(ServerMessageType type) {
        this.type = type;
    }
}

public class ServerMessageSecret : ServerMessage
{
    public string secret;

    public ServerMessageSecret(string secret) : base(ServerMessageType.secret) {
        this.secret = secret;
    }
}

public class ServerMessageJoinedGame : ServerMessage
{
    public ObjectData player;

    public ServerMessageJoinedGame(ObjectData player) : base(ServerMessageType.joinedGame) {
        this.player = player;
    }
}

public class ServerMessageMovements : ServerMessage
{
    public ObjectData[] players;

    public ServerMessageMovements(ObjectData[] players) : base(ServerMessageType.movements) {
        this.players = players;
    }
}

public class ServerMessageGameState : ServerMessage
{
    public string id;
    public ObjectData[] players;

    public ServerMessageGameState(string id, ObjectData[] players) : base(ServerMessageType.gameState) {
        this.id = id;
        this.players = players;
    }
}

public class ServerMessageLeftGame : ServerMessage
{
    public string id;

    public ServerMessageLeftGame(string id) : base(ServerMessageType.leftGame) {
        this.id = id;
    }
}