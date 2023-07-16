using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    private static MessageHandler _current;

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
    }

    public delegate void OnMessageJoinedGameHandler(MessageJoinedGame messageJoinedGame);
    public delegate void OnMessagePlayerMovedHandler(MessagePlayerMoved messagePlayerMoved);
    public delegate void OnMessageLeftGameHandler(MessageLeftGame messageLeftGame);
    public delegate void OnMessageGameStateHandler(MessageGameState messageGameState);
    public delegate void OnMessageUsedAbilityHandler(MessageUsedAbility messageUsedAbility);
    public delegate void OnMessageDespawnObjectHandler(MessageDespawnObject messageDespawnObject);
    public delegate void OnMessageSpawnNodesHandler(MessageSpawnNodes messageSpawnNodes);
    public delegate void OnMessageNPCMovedHandler(MessageNPCMoved messageNPCMoved);
    public delegate void OnMessageSpawnNPCsHandler(MessageSpawnNPCs messageSpawnNPCs);
    public delegate void OnMessageInventoryAddHandler(MessageInventoryAdd messageInventoryAdd);
    public delegate void OnMessageInventoryRemoveHandler(MessageInventoryRemove messageInventoryRemove);
    public delegate void OnMessageCraftedHandler(MessageCrafted messageCrafted);
    public delegate void OnMessageHealthChangedHandler(MessageHealthChanged messageHealthChanged);
    public delegate void OnMessageChannelHandler(MessageChannel messageChannel);
    public delegate void OnMessageCastHandler(MessageCast messageCast);
    public delegate void OnMessageStopActivityHandler(MessageStopActivity messageStopActivity);
    public delegate void OnMessageExperienceChangedHandler(MessageExperienceChanged messageExperienceChanged);
    public delegate void OnMessageEquipedHandler(MessageEquiped messageEquiped);

    public event OnMessageJoinedGameHandler OnMessageJoinedGameEvent;
    public event OnMessagePlayerMovedHandler OnMessagePlayerMovedEvent;
    public event OnMessageLeftGameHandler OnMessageLeftGameEvent;
    public event OnMessageGameStateHandler OnMessageGameStateEvent;
    public event OnMessageUsedAbilityHandler OnMessageUsedAbilityEvent;
    public event OnMessageDespawnObjectHandler OnMessageDespawnObjectEvent;
    public event OnMessageSpawnNodesHandler OnMessageSpawnNodesEvent;
    public event OnMessageNPCMovedHandler OnMessageNPCMovedEvent;
    public event OnMessageSpawnNPCsHandler OnMessageSpawnNPCsEvent;
    public event OnMessageInventoryAddHandler OnMessageInventoryAddEvent;
    public event OnMessageInventoryRemoveHandler OnMessageInventoryRemoveEvent;
    public event OnMessageCraftedHandler OnMessageCraftedEvent;
    public event OnMessageHealthChangedHandler OnMessageHealthChangedEvent;
    public event OnMessageChannelHandler OnMessageChannelEvent;
    public event OnMessageCastHandler OnMessageCastEvent;
    public event OnMessageStopActivityHandler OnMessageStopActivityEvent;
    public event OnMessageExperienceChangedHandler OnMessageExperienceChangedEvent;
    public event OnMessageEquipedHandler OnMessageEquipedEvent;

    public OnMessageJoinedGameHandler OnMessageJoinedGame => OnMessageJoinedGameEvent;
    public OnMessagePlayerMovedHandler OnMessagePlayerMoved => OnMessagePlayerMovedEvent;
    public OnMessageLeftGameHandler OnMessageLeftGame => OnMessageLeftGameEvent;
    public OnMessageGameStateHandler OnMessageGameState => OnMessageGameStateEvent;
    public OnMessageUsedAbilityHandler OnMessageUsedAbility => OnMessageUsedAbilityEvent;
    public OnMessageDespawnObjectHandler OnMessageDespawnObject => OnMessageDespawnObjectEvent;
    public OnMessageSpawnNodesHandler OnMessageSpawnNodes => OnMessageSpawnNodesEvent;
    public OnMessageNPCMovedHandler OnMessageNPCMoved => OnMessageNPCMovedEvent;
    public OnMessageSpawnNPCsHandler OnMessageSpawnNPCs => OnMessageSpawnNPCsEvent;
    public OnMessageInventoryAddHandler OnMessageInventoryAdd => OnMessageInventoryAddEvent;
    public OnMessageInventoryRemoveHandler OnMessageInventoryRemove => OnMessageInventoryRemoveEvent;
    public OnMessageCraftedHandler OnMessageCrafted => OnMessageCraftedEvent;
    public OnMessageHealthChangedHandler OnMessageHealthChanged => OnMessageHealthChangedEvent;
    public OnMessageChannelHandler OnMessageChannel => OnMessageChannelEvent;
    public OnMessageCastHandler OnMessageCast => OnMessageCastEvent;
    public OnMessageStopActivityHandler OnMessageStopActivity => OnMessageStopActivityEvent;
    public OnMessageExperienceChangedHandler OnMessageExperienceChanged => OnMessageExperienceChangedEvent;
    public OnMessageEquipedHandler OnMessageEquiped => OnMessageEquipedEvent;

    public static MessageHandler Current => _current;
}
