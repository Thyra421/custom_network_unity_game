import { players } from "../api"
import { Player } from "../components/player"
import { PlayerData } from "./player_data"

enum ServerMessageType {
    me, joinedGame, leftGame, positions
}

class ServerMessage {
    readonly type: ServerMessageType

    protected constructor(type: ServerMessageType) {
        this.type = type
    }
}

class ServerMessageMe extends ServerMessage {
    readonly id: string

    constructor(id: string) {
        super(ServerMessageType.me)
        this.id = id
    }
}

class ServerMessageJoinedGame extends ServerMessage {
    readonly player: PlayerData

    constructor(player: PlayerData) {
        super(ServerMessageType.joinedGame)
        this.player = player
    }
}

class ServerMessagePositions extends ServerMessage {
    readonly players: PlayerData[]

    constructor() {
        super(ServerMessageType.positions)
        this.players = players.getPlayers().map((p: Player) => p.data)
    }
}

class ServerMessageLeftGame extends ServerMessage {
    readonly id: string

    constructor(id: string) {
        super(ServerMessageType.leftGame)
        this.id = id
    }
}

export {
    ServerMessageType, ServerMessage, ServerMessageMe, ServerMessagePositions, ServerMessageJoinedGame, ServerMessageLeftGame
}