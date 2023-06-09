import dgram from 'dgram'
import { UDPClient } from '../components/udp_client'
import { Client } from '../components/client'
import { SERVER_PORT, UPDATE_FREQUENCY } from '../config'
import { clients, players } from '../api'
import { ClientMessage, ClientMessagePosition, ClientMessageType } from '../data/client_message'
import { Player } from '../components/player'
import { PlayerData } from '../data/player_data'
import { ServerMessagePosition } from '../data/server_message'

class UDPServer {
    readonly udpServer: dgram.Socket = dgram.createSocket('udp4')

    start = () => {
        this.udpServer.on('message', this.onMessage)

        this.udpServer.bind(SERVER_PORT)

        setInterval(this.sync, 1000 / UPDATE_FREQUENCY)
        console.log(`[UDP server] bound on port ${SERVER_PORT}`)
    }

    private onMessage = (msg: Buffer, rinfo: dgram.RemoteInfo): void => {
        const message: ClientMessage = JSON.parse(msg.toString())

        this.addClientIfNotExist(rinfo.address, rinfo.port, message.id)

        switch (message.type) {
            case ClientMessageType.position:
                this.onMessagePosition(message as ClientMessagePosition)
                break
        }
    }

    private addClientIfNotExist = (address: string, port: number, id: string): void => {
        let client: Client = clients.findFromAddressAndPort(address, port)

        if (client == null) {
            client = clients.findFromId(id)
            client.udp = new UDPClient(address, port)
        }
    }

    private onMessagePosition = (messagePosition: ClientMessagePosition): void => {
        const player: Player = players.findFromId(messagePosition.id)
        if (player != null)
            player.data.position = messagePosition.position
    }

    private sync = () => {
        const positions: PlayerData[] = players.getPlayers().map(p => p.data)
        clients.sendToAllPlayersUDP(new ServerMessagePosition(positions))
    }
}

export { UDPServer }
