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
    public string itemName;
    public int amount;

    public MessageInventoryAdd(string itemName, int amount) {
        this.itemName = itemName;
        this.amount = amount;
    }
}

public struct MessageInventoryRemove
{
    public string itemName;
    public int amount;

    public MessageInventoryRemove(string itemName, int amount) {
        this.itemName = itemName;
        this.amount = amount;
    }
}

public enum MessageErrorType
{
    inventoryFull, uniqueItem, objectNotFound
}

public struct MessageError
{
    public MessageErrorType type;

    public MessageError(MessageErrorType type) {
        this.type = type;
    }
}

public struct MessageCraft
{
    public ItemStackData[] reagents;
    public ItemStackData[] outcomes;

    public MessageCraft(ItemStackData[] reagents, ItemStackData[] outcomes) {
        this.reagents = reagents;
        this.outcomes = outcomes;
    }
}