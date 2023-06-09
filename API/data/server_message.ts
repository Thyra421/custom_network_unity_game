import { PlayerData } from "./player_data"

enum ServerMessageType {
    joinedGame, position
}

class ServerMessage {
    readonly type: ServerMessageType

    protected constructor(type: ServerMessageType) {
        this.type = type
    }
}

class ServerMessageJoinedGame extends ServerMessage {
    readonly player: PlayerData

    constructor(player: PlayerData) {
        super(ServerMessageType.joinedGame)
        this.player = player
    }
}

class ServerMessagePosition extends ServerMessage {
    readonly players: PlayerData[]

    constructor(players: PlayerData[]) {
        super(ServerMessageType.position)
        this.players = players
    }
}

export { ServerMessageType, ServerMessage, ServerMessagePosition, ServerMessageJoinedGame }