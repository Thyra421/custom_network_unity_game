import { WebSocket } from 'ws'
import { ServerMessage } from '../data/server_message'

class TCPClient {
    readonly socket: WebSocket

    constructor(socket: WebSocket) {
        this.socket = socket
    }

    send = (message: ServerMessage) => {
        this.socket.send(JSON.stringify(message))
    }
}

export { TCPClient }