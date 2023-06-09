public enum ServerMessageType
{
    joinedGame, position
}

public class ServerMessage
{
    public ServerMessageType type;
}

public class ServerMessageJoinedGame : ServerMessage
{
    public PlayerData player;
}

public class ServerMessagePosition : ServerMessage
{
    public PlayerData[] players;
}

public delegate void OnServerMessageJoinedGameHandler(ServerMessageJoinedGame messageJoinedGame);
public delegate void OnServerMessagePositionHandler(ServerMessagePosition messagePosition);