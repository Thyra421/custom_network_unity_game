import { Vector3 } from "./vector3"

enum ClientMessageType { authenticate, position }

abstract class ClientMessage {
    readonly id: string
    readonly type: ClientMessageType
}

class ClientMessagePosition extends ClientMessage {
    readonly position: Vector3
}

class ClientMessageAuthenticate extends ClientMessage { }

export { ClientMessageType, ClientMessage, ClientMessagePosition, ClientMessageAuthenticate }