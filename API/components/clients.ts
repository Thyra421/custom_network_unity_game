import { players } from "../api";
import { ServerMessage } from "../data/server_message";
import { Client } from "./client";
import { WebSocket } from "ws"
import { v4 as uuidv4 } from 'uuid'


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

    create = (): Client => {
        const id: string = uuidv4()
        const newClient: Client = new Client(id)
        this.clients.push(newClient)
        console.log("[CLIENTS] " + this.clients.map(c => c.id))
        return newClient
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