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

public struct MessagePlayerMoved
{
    public PlayerData[] players;

    public MessagePlayerMoved(PlayerData[] players) {
        this.players = players;
    }
}

public struct MessageHealthChanged
{
    public string id;
    public int currentHealth;
    public int maxHealth;

    public MessageHealthChanged(string id, int currentHealth, int maxHealth) {
        this.id = id;
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
    public string id;
    public string animationName;

    public MessageTriggerAnimation(string id, string animationName) {
        this.id = id;
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
    public string id;
    public string activityName;
    public float castTimeInSeconds;

    public MessageCast(string id, string activityName, float castTimeInSeconds) {
        this.id = id;
        this.castTimeInSeconds = castTimeInSeconds;
        this.activityName = activityName;
    }
}

public struct MessageChannel
{
    public string id;
    public float intervalTimeInSeconds;
    public int ticks;
    public string activityName;

    public MessageChannel(string id, string activityName, int ticks, float intervalTimeInSeconds) {
        this.id = id;
        this.activityName = activityName;
        this.ticks = ticks;
        this.intervalTimeInSeconds = intervalTimeInSeconds;
    }
}

public struct MessageStopActivity
{
    public string id;

    public MessageStopActivity(string id) {
        this.id = id;
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


public struct MessageNPCMoved
{
    public NPCData[] NPCs;

    public MessageNPCMoved(NPCData[] NPCs) {
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

public struct MessageVFXMoved
{
    public VFXMovementData[] VFXs;

    public MessageVFXMoved(VFXMovementData[] vFXs) {
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

public struct MessageWeaponExperienceChanged
{
    public string weaponName;
    public int currentLevel;
    public float currentRatio;

    public MessageWeaponExperienceChanged(string weaponName, int currentLevel, float currentRatio) {
        this.weaponName = weaponName;
        this.currentLevel = currentLevel;
        this.currentRatio = currentRatio;
    }
}

public struct MessageStatisticsChanged
{
    public StatisticData[] statisticDatas;

    public MessageStatisticsChanged(StatisticData[] statisticDatas) {
        this.statisticDatas = statisticDatas;
    }
}

public enum MessageErrorType
{
    inventoryFull, notEnoughInventorySpace, uniqueItem, objectNotFound, abilityNotFound, notEnoughResources, tooFarAway, cantWhileMoving, busy, inCooldown, cantDoThat
}

public struct MessageError
{
    public MessageErrorType type;

    public MessageError(MessageErrorType type) {
        this.type = type;
    }
}

#endregion LOCAL