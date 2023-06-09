import express from 'express'
import cors from 'cors'
import { v4 as uuidv4 } from 'uuid'
import { Client } from '../components/client'
import { ServerMessageJoinedGame } from '../data/server_message'
import { clients, players } from '../api'
import { Player } from '../components/player'

class AppServer {
    readonly appServer: express.Application = express().use(cors())

    start = () => {
        this.appServer.get("/connect", (req, res) => {
            const id: string = uuidv4()
            const client: Client = new Client(id)
            clients.add(client)
            res.send({ id: id })
        })

        this.appServer.get("/play", (req, res) => {
            const id: string = req.query.id as string
            const newPlayer: Player = players.create(id)
            clients.sendToAllPlayersTCP(new ServerMessageJoinedGame(newPlayer.data))
            res.send({ players: players })
        })
    }
}

export { AppServer }
