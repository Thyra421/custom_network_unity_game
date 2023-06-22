using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    //private static MessageHandler _current;

    //private void Awake() {
    //    if (_current == null) {
    //        _current = this;
    //    } else
    //        Destroy(gameObject);
    //}

    public delegate void OnMessageJoinedGameHandler(MessageJoinedGame messageJoinedGame);
    public delegate void OnMessageMovedHandler(MessageMoved messageMoved);
    public delegate void OnMessageLeftGameHandler(MessageLeftGame messageLeftGame);
    public delegate void OnMessageGameStateHandler(MessageGameState messageGameState);
    public delegate void OnMessageAttackedHandler(MessageAttacked messageAttacked);
    public delegate void OnMessageDamageHandler(MessageDamage messageDamage);
    public delegate void OnMessageDespawnObjectHandler(MessageDespawnObject messageDespawnObject);
    public delegate void OnMessageSpawnNodesHandler(MessageSpawnNodes messageSpawnNodes);
    public delegate void OnMessageInventoryAddHandler(MessageInventoryAdd messageInventoryAdd);
    public delegate void OnMessageLootedHandler(MessageLooted messageLooted);

    public static OnMessageJoinedGameHandler onMessageJoinedGame;
    public static OnMessageMovedHandler onMessageMoved;
    public static OnMessageLeftGameHandler onMessageLeftGame;
    public static OnMessageGameStateHandler onMessageGameState;
    public static OnMessageAttackedHandler onMessageAttacked;
    public static OnMessageDamageHandler onMessageDamage;
    public static OnMessageDespawnObjectHandler onMessageDespawnObject;
    public static OnMessageSpawnNodesHandler onMessageSpawnNodes;
    public static OnMessageInventoryAddHandler onMessageInventoryAdd;
    public static OnMessageLootedHandler onMessageLooted;

    //public static MessageHandler Current => _current;
}
