using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    public delegate void OnMessageJoinedGameHandler(MessageJoinedGame messageJoinedGame);
    public delegate void OnMessageMovedHandler(MessageMoved messageMoved);
    public delegate void OnMessageLeftGameHandler(MessageLeftGame messageLeftGame);
    public delegate void OnMessageGameStateHandler(MessageGameState messageGameState);
    public delegate void OnMessageAttackedHandler(MessageAttacked messageAttacked);
    public delegate void OnMessageDamageHandler(MessageDamage messageDamage);
    public delegate void OnMessageDespawnObjectHandler(MessageDespawnObject messageDespawnObject);
    public delegate void OnMessageSpawnNodesHandler(MessageSpawnNodes messageSpawnNodes);
    public delegate void OnMessageInventoryAddHandler(MessageInventoryAdd messageInventoryAdd);
    public delegate void OnMessageInventoryRemoveHandler(MessageInventoryRemove messageInventoryRemove);
    public delegate void OnMessageLootedHandler(MessageLooted messageLooted);
    public delegate void OnMessageCraftedHandler(MessageCrafted messageCrafted);

    private static event OnMessageJoinedGameHandler _onMessageJoinedGame;
    private static event OnMessageMovedHandler _onMessageMoved;
    private static event OnMessageLeftGameHandler _onMessageLeftGame;
    private static event OnMessageGameStateHandler _onMessageGameState;
    private static event OnMessageAttackedHandler _onMessageAttacked;
    private static event OnMessageDamageHandler _onMessageDamage;
    private static event OnMessageDespawnObjectHandler _onMessageDespawnObject;
    private static event OnMessageSpawnNodesHandler _onMessageSpawnNodes;
    private static event OnMessageInventoryAddHandler _onMessageInventoryAdd;
    private static event OnMessageInventoryRemoveHandler _onMessageInventoryRemove;
    private static event OnMessageLootedHandler _onMessageLooted;
    private static event OnMessageCraftedHandler _onMessageCrafted;

    public static event OnMessageJoinedGameHandler OnMessageJoinedGameEvent {
        add => _onMessageJoinedGame += value;
        remove => _onMessageJoinedGame -= value;
    }

    public static event OnMessageMovedHandler OnMessageMovedEvent {
        add => _onMessageMoved += value;
        remove => _onMessageMoved -= value;
    }

    public static event OnMessageLeftGameHandler OnMessageLeftGameEvent {
        add => _onMessageLeftGame += value;
        remove => _onMessageLeftGame -= value;
    }

    public static event OnMessageGameStateHandler OnMessageGameStateEvent {
        add => _onMessageGameState += value;
        remove => _onMessageGameState -= value;
    }

    public static event OnMessageAttackedHandler OnMessageAttackedEvent {
        add => _onMessageAttacked += value;
        remove => _onMessageAttacked -= value;
    }

    public static event OnMessageDamageHandler OnMessageDamageEvent {
        add => _onMessageDamage += value;
        remove => _onMessageDamage -= value;
    }

    public static event OnMessageDespawnObjectHandler OnMessageDespawnObjectEvent {
        add => _onMessageDespawnObject += value;
        remove => _onMessageDespawnObject -= value;
    }

    public static event OnMessageSpawnNodesHandler OnMessageSpawnNodesEvent {
        add => _onMessageSpawnNodes += value;
        remove => _onMessageSpawnNodes -= value;
    }

    public static event OnMessageInventoryAddHandler OnMessageInventoryAddEvent {
        add => _onMessageInventoryAdd += value;
        remove => _onMessageInventoryAdd -= value;
    }

    public static event OnMessageInventoryRemoveHandler OnMessageInventoryRemoveEvent {
        add => _onMessageInventoryRemove += value;
        remove => _onMessageInventoryRemove -= value;
    }

    public static event OnMessageLootedHandler OnMessageLootedEvent {
        add => _onMessageLooted += value;
        remove => _onMessageLooted -= value;
    }

    public static event OnMessageCraftedHandler OnMessageCraftedEvent {
        add => _onMessageCrafted += value;
        remove => _onMessageCrafted -= value;
    }

    public static OnMessageJoinedGameHandler OnMessageJoinedGame => _onMessageJoinedGame;

    public static OnMessageMovedHandler OnMessageMoved => _onMessageMoved;

    public static OnMessageLeftGameHandler OnMessageLeftGame => _onMessageLeftGame;

    public static OnMessageGameStateHandler OnMessageGameState => _onMessageGameState;

    public static OnMessageAttackedHandler OnMessageAttacked => _onMessageAttacked;

    public static OnMessageDamageHandler OnMessageDamage => _onMessageDamage;

    public static OnMessageDespawnObjectHandler OnMessageDespawnObject => _onMessageDespawnObject;

    public static OnMessageSpawnNodesHandler OnMessageSpawnNodes => _onMessageSpawnNodes;

    public static OnMessageInventoryAddHandler OnMessageInventoryAdd => _onMessageInventoryAdd;

    public static OnMessageInventoryRemoveHandler OnMessageInventoryRemove => _onMessageInventoryRemove;

    public static OnMessageLootedHandler OnMessageLooted => _onMessageLooted;

    public static OnMessageCraftedHandler OnMessageCrafted => _onMessageCrafted;
}
