#region PLAYERS

public struct MessageSecret
{
    public string secret;

    public MessageSecret(string secret) {
        this.secret = secret;
    }
}

public struct MessageJoinedGame
{
    public PlayerData player;

    public MessageJoinedGame(PlayerData player) {
        this.player = player;
    }
}

public struct MessageLeftGame
{
    public string id;

    public MessageLeftGame(string id) {
        this.id = id;
    }
}

public struct MessageGameState
{
    public string id;
    public PlayerData[] players;

    public MessageGameState(string id, PlayerData[] players) {
        this.id = id;
        this.players = players;
    }
}

public struct MessagePlayersMoved
{
    public PlayerMovementData[] players;

    public MessagePlayersMoved(PlayerMovementData[] players) {
        this.players = players;
    }
}

public struct MessageHealthChanged
{
    public CharacterData character;
    public int currentHealth;
    public int maxHealth;

    public MessageHealthChanged(CharacterData character, int currentHealth, int maxHealth) {
        this.character = character;
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
    }
}

public struct MessageEquiped
{
    public string id;
    public string weaponName;

    public MessageEquiped(string id, string weaponName) {
        this.id = id;
        this.weaponName = weaponName;
    }
}

public struct MessageTriggerAnimation
{
    public CharacterData character;
    public string animationName;

    public MessageTriggerAnimation(CharacterData character, string animationName) {
        this.character = character;
        this.animationName = animationName;
    }
}

#region ALTERATIONS

public struct MessageAddAlteration
{
    public AlterationData alteration;

    public MessageAddAlteration(AlterationData alteration) {
        this.alteration = alteration;
    }
}

public struct MessageRefreshAlteration
{
    public AlterationData alteration;

    public MessageRefreshAlteration(AlterationData alteration) {
        this.alteration = alteration;
    }
}

public struct MessageRemoveAlteration
{
    public AlterationData alteration;

    public MessageRemoveAlteration(AlterationData alteration) {
        this.alteration = alteration;
    }
}

#endregion ALTERATIONS

#region ACTIVITY

public struct MessageCast
{
    public CharacterData character;
    public string activityName;
    public float castTimeInSeconds;

    public MessageCast(CharacterData character, string activityName, float castTimeInSeconds) {
        this.character = character;
        this.castTimeInSeconds = castTimeInSeconds;
        this.activityName = activityName;
    }
}

public struct MessageChannel
{
    public CharacterData character;
    public float intervalTimeInSeconds;
    public int ticks;
    public string activityName;

    public MessageChannel(CharacterData character, string activityName, int ticks, float intervalTimeInSeconds) {
        this.character = character;
        this.activityName = activityName;
        this.ticks = ticks;
        this.intervalTimeInSeconds = intervalTimeInSeconds;
    }
}

public struct MessageStopActivity
{
    public CharacterData character;


    public MessageStopActivity(CharacterData character) {
        this.character = character;
    }
}

#endregion ACTIVITY

#endregion PLAYERS

#region NODES

public struct MessageSpawnNodes
{
    public NodeData[] nodes;

    public MessageSpawnNodes(NodeData[] nodes) {
        this.nodes = nodes;
    }
}

public struct MessageDespawnNode
{
    public string id;

    public MessageDespawnNode(string id) {
        this.id = id;
    }
}

#endregion NODES

#region NPCS

public struct MessageSpawnNPCs
{
    public NPCData[] NPCs;

    public MessageSpawnNPCs(NPCData[] NPCs) {
        this.NPCs = NPCs;
    }
}

public struct MessageNPCsMoved
{
    public NPCMovementData[] NPCs;

    public MessageNPCsMoved(NPCMovementData[] NPCs) {
        this.NPCs = NPCs;
    }
}

#endregion NPCS

#region VFXS

public struct MessageSpawnVFX
{
    public VFXData VFX;

    public MessageSpawnVFX(VFXData VFX) {
        this.VFX = VFX;
    }
}

public struct MessageDespawnVFX
{
    public string id;

    public MessageDespawnVFX(string id) {
        this.id = id;
    }
}

public struct MessageVFXsMoved
{
    public VFXMovementData[] VFXs;

    public MessageVFXsMoved(VFXMovementData[] vFXs) {
        VFXs = vFXs;
    }
}

#endregion VFXS

#region LOCAL

public struct MessageUsedAbility
{
    public string abilityName;

    public MessageUsedAbility(string abilityName) {
        this.abilityName = abilityName;
    }
}

public struct MessageInventoryAdd
{
    public ItemStackData data;

    public MessageInventoryAdd(ItemStackData data) {
        this.data = data;
    }
}

public struct MessageInventoryRemove
{
    public ItemStackData data;

    public MessageInventoryRemove(ItemStackData data) {
        this.data = data;
    }
}

public struct MessageCrafted
{
    public ItemStackData[] reagents;
    public ItemStackData outcome;

    public MessageCrafted(ItemStackData[] reagents, ItemStackData outcome) {
        this.reagents = reagents;
        this.outcome = outcome;
    }
}

public struct MessageExperienceChanged
{
    public ExperienceData[] experiences;

    public MessageExperienceChanged(ExperienceData[] experiences) {
        this.experiences = experiences;
    }
}

public struct MessageStatisticsChanged
{
    public StatisticData[] statisticDatas;

    public MessageStatisticsChanged(StatisticData[] statisticDatas) {
        this.statisticDatas = statisticDatas;
    }
}

public struct MessageStatesChanged
{
    public StateData[] stateDatas;

    public MessageStatesChanged(StateData[] stateDatas) {
        this.stateDatas = stateDatas;
    }
}

public enum MessageErrorType
{
    InventoryFull,
    NotEnoughInventorySpace,
    UniqueItem,
    ObjectNotFound,
    AbilityNotFound,
    NotEnoughResources,
    TooFarAway,
    CantWhileMoving,
    Busy,
    InCooldown,
    CantDoThat,
    CantWhileStunned
}

public struct MessageError
{
    public MessageErrorType type;

    public MessageError(MessageErrorType type) {
        this.type = type;
    }
}

#endregion LOCAL