import { Player } from "./player";

class Players {
    private readonly players: Player[] = []

    findFromId = (id: string): Player => {
        return this.players.find((p: Player) => p.data.id == id)
    }

    create = (id: string): Player => {
        const newPlayer: Player = new Player(id)
        this.players.push(newPlayer)
        return newPlayer
    }

    getPlayers = (): Player[] => {
        return this.players
    }
}

export { Players }