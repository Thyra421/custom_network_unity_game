using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    public static MessageHandler Current { get; private set; }

    public delegate void OnMessageJoinedGameHandler(MessageJoinedGame messageJoinedGame);
    public delegate void OnMessagePlayerMovedHandler(MessagePlayerMoved messagePlayerMoved);
    public delegate void OnMessageLeftGameHandler(MessageLeftGame messageLeftGame);
    public delegate void OnMessageGameStateHandler(MessageGameState messageGameState);
    public delegate void OnMessageUsedAbilityHandler(MessageUsedAbility messageUsedAbility);
    public delegate void OnMessageDespawnNodeHandler(MessageDespawnNode messageDespawnNode);
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
    public delegate void OnMessageSpawnVFXHandler(MessageSpawnVFX messageSpawnVFX);
    public delegate void OnMessageDespawnVFXHandler(MessageDespawnVFX messageDespawnVFX);
    public delegate void OnMessageVFXMovedHandler(MessageVFXMoved messageVFXMoved);
    public delegate void OnMessageTriggerAnimationHandler(MessageTriggerAnimation messageTriggerAnimation);

    public event OnMessageJoinedGameHandler OnMessageJoinedGameEvent;
    public event OnMessagePlayerMovedHandler OnMessagePlayerMovedEvent;
    public event OnMessageLeftGameHandler OnMessageLeftGameEvent;
    public event OnMessageGameStateHandler OnMessageGameStateEvent;
    public event OnMessageUsedAbilityHandler OnMessageUsedAbilityEvent;
    public event OnMessageDespawnNodeHandler OnMessageDespawnNodeEvent;
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
    public event OnMessageSpawnVFXHandler OnMessageSpawnVFXEvent;
    public event OnMessageDespawnVFXHandler OnMessageDespawnVFXEvent;
    public event OnMessageVFXMovedHandler OnMessageVFXMovedEvent;
    public event OnMessageTriggerAnimationHandler OnMessageTriggerAnimationEvent;

    public OnMessageJoinedGameHandler OnMessageJoinedGame => OnMessageJoinedGameEvent;
    public OnMessagePlayerMovedHandler OnMessagePlayerMoved => OnMessagePlayerMovedEvent;
    public OnMessageLeftGameHandler OnMessageLeftGame => OnMessageLeftGameEvent;
    public OnMessageGameStateHandler OnMessageGameState => OnMessageGameStateEvent;
    public OnMessageUsedAbilityHandler OnMessageUsedAbility => OnMessageUsedAbilityEvent;
    public OnMessageDespawnNodeHandler OnMessageDespawnNode => OnMessageDespawnNodeEvent;
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
    public OnMessageSpawnVFXHandler OnMessageSpawnVFX => OnMessageSpawnVFXEvent;
    public OnMessageDespawnVFXHandler OnMessageDespawnVFX => OnMessageDespawnVFXEvent;
    public OnMessageVFXMovedHandler OnMessageVFXMoved => OnMessageVFXMovedEvent;
    public OnMessageTriggerAnimationHandler OnMessageTriggerAnimation => OnMessageTriggerAnimationEvent;

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
    }
}
