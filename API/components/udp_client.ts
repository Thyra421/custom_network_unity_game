import { udpServer } from "../api"
import { ServerMessage } from "../data/server_message"

class UDPClient {
    readonly port: number
    readonly address: string

    constructor(address: string, port: number) {
        this.port = port
        this.address = address
    }

    send = (udpMessage: ServerMessage) => {
        udpServer.udpServer.send(JSON.stringify(udpMessage), this.port, this.address)
    }
}

export { UDPClient }