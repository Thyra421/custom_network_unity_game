import { PlayerData } from "../data/player_data"

class Player {
    data: PlayerData

    constructor(id: string) {
        this.data = new PlayerData(id)
    }
}

export { Player }