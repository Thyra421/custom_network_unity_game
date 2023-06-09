import { Vector3 } from "./vector3"

class PlayerData {
    readonly id: string
    position: Vector3

    constructor(id: string) {
        this.id = id
        this.position = Vector3.zero()
    }
}

export { PlayerData }