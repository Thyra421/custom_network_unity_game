namespace TestUDP;

public enum ServerMessageType
{
    secret, // HTTP
    gameState, // HTTP
    joinedGame, // TCP
    leftGame, // TCP
    positions // UDP
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

public class ServerMessagePositions : ServerMessage
{
    public ObjectData[] players;

    public ServerMessagePositions(ObjectData[] players) : base(ServerMessageType.positions) {
        this.players = players;
    }
}

public class ServerMessageGameState : ServerMessage
{
    public string id;
    public ObjectData[] players;

    public ServerMessageGameState(string id, ObjectData[] players) : base(ServerMessageType.secret) {
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