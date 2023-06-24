public struct MessageSecret
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

public struct MessageDamage
{
    public string idFrom;
    public string idTo;

    public MessageDamage(string idFrom, string idTo) {
        this.idFrom = idFrom;
        this.idTo = idTo;
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

public enum MessageErrorType
{
    inventoryFull, notEnoughInventorySpace, uniqueItem, objectNotFound, notEnoughResources
}

public struct MessageError
{
    public MessageErrorType type;

    public MessageError(MessageErrorType type) {
        this.type = type;
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