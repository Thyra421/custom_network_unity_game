using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    private static MessageHandler _current;

    private void Awake() {
        if (_current == null) {
            _current = this;
        } else
            Destroy(gameObject);
    }

    public delegate void OnServerMessageJoinedGameHandler(ServerMessageJoinedGame messageJoinedGame);
    public delegate void OnServerMessagePositionsHandler(ServerMessagePositions messagePositions);
    public delegate void OnServerMessageLeftGameHandler(ServerMessageLeftGame messageLeftGame);

    public OnServerMessageJoinedGameHandler onServerMessageJoinedGame;
    public OnServerMessagePositionsHandler onServerMessagePositions;
    public OnServerMessageLeftGameHandler onServerMessageLeftGame;

    public static MessageHandler Current => _current;
}
