class Vector3 {
    readonly x: number
    readonly y: number
    readonly z: number

    static zero = (): Vector3 => { return { x: 0, y: 0, z: 0 } }
}

export { Vector3 }