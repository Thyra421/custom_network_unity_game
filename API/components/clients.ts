import { players } from "../api";
import { ServerMessage } from "../data/server_message";
import { Client } from "./client";
import { WebSocket } from "ws"

class Clients {
    private readonly clients: Client[] = []

    findFromSocket = (socket: WebSocket): Client => {
        return this.clients.find((c: Client) => {
            return c.tcp?.socket == socket
        })
    }

    findFromAddressAndPort = (address: string, port: number): Client => {
        return this.clients.find((c: Client) => {
            return c.udp?.port == port && c.udp?.address == address
        })
    }

    findFromId = (id: string): Client => {
        return this.clients.find((c: Client) => c.id == id)
    }

    add = (client: Client): void => {
        this.clients.push(client)
        console.log("[CLIENTS] " + this.clients.map(c => c.id))
    }

    sendToAllPlayersUDP = (udpMessage: ServerMessage): void => {
        this.clients.forEach((c: Client) => {
            if (c.udp != null && players.findFromId(c.id) != null)
                c.udp.send(udpMessage)
        })
    }

    sendToAllPlayersTCP = (tcpMessage: ServerMessage): void => {
        this.clients.forEach((c: Client) => {
            if (c.udp != null && players.findFromId(c.id) != null)
                c.tcp.send(tcpMessage)
        })
    }
}

export { Clients }