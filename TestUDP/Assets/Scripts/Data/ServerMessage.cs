public enum ServerMessageType
{
    me, joinedGame, leftGame, positions
}

public class ServerMessage
{
    public ServerMessageType type;
}

public class ServerMessageMe : ServerMessage
{
    public string id;
}

public class ServerMessageJoinedGame : ServerMessage
{
    public PlayerData player;
}

public class ServerMessagePositions : ServerMessage
{
    public PlayerData[] players;
}

public class ServerMessageLeftGame : ServerMessage
{
    public string id;
}

public delegate void OnServerMessageJoinedGameHandler(ServerMessageJoinedGame messageJoinedGame);
public delegate void OnServerMessagePositionsHandler(ServerMessagePositions messagePositions);
public delegate void OnServerMessageLeftGameHandler(ServerMessageLeftGame messageLeftGame);