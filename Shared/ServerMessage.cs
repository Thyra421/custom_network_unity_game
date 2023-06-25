﻿public struct MessageSecret
{
    public string secret;

    public MessageSecret(string secret) {
        this.secret = secret;
    }
}

public struct MessageLooted
{
    public string id;

    public MessageLooted(string id) {
        this.id = id;
    }
}

public struct MessageJoinedGame
{
    public PlayerData player;

    public MessageJoinedGame(PlayerData player) {
        this.player = player;
    }
}

public struct MessageMoved
{
    public PlayerData[] players;

    public MessageMoved(PlayerData[] players) {
        this.players = players;
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

public struct MessageSpawnNodes
{
    public NodeData[] nodes;

    public MessageSpawnNodes(NodeData[] nodes) {
        this.nodes = nodes;
    }
}

public struct MessageDespawnObject
{
    public string id;

    public MessageDespawnObject(string id) {
        this.id = id;
    }
}

public struct MessageLeftGame
{
    public string id;

    public MessageLeftGame(string id) {
        this.id = id;
    }
}

public struct MessageAttacked
{
    public string id;

    public MessageAttacked(string id) {
        this.id = id;
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

public struct MessageError
{
    public MessageErrorType type;

    public MessageError(MessageErrorType type) {
        this.type = type;
    }

    public enum MessageErrorType
    {
        inventoryFull, notEnoughInventorySpace, uniqueItem, objectNotFound, notEnoughResources, tooFarAway
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

//public struct MessageStatsChanged
//{
//    public StatEntry[] entries;

//    public MessageStatsChanged(StatEntry[] entries) {
//        this.entries = entries;
//    }

//    public enum Stat
//    {
//        Health, MaxHealth
//    }

//    public struct StatEntry
//    {
//        public Stat stat;
//        public int value;

//        public StatEntry(Stat stat, int value) {
//            this.stat = stat;
//            this.value = value;
//        }
//    }
//}

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
