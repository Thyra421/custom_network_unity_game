﻿public struct MessageSecret
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

public struct MessageSpawnObjects
{
    public ObjectData[] nodes;

    public MessageSpawnObjects(ObjectData[] nodes) {
        this.nodes = nodes;
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

public struct MessagePickedUp
{
    public string playerId;
    public string objectId;

    public MessagePickedUp(string playerId, string objectId) : this() {
        this.playerId = playerId;
        this.objectId = objectId;
    }

}