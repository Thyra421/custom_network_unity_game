import express from 'express'
import cors from 'cors'
import { ServerMessageJoinedGame, ServerMessageMe, ServerMessagePositions } from '../data/server_message'
import { clients, players } from '../api'
import { Player } from '../components/player'
import { Client } from '../components/client'

class AppServer {
    readonly appServer: express.Application = express().use(cors())

    start = () => {
        this.appServer.get("/connect", (req, res) => {
            const newClient: Client = clients.create()
            res.send(new ServerMessageMe(newClient.id))
        })

        this.appServer.get("/play", (req, res) => {
            const id: string = req.query.id as string
            const newPlayer: Player = players.create(id)
            clients.sendToAllPlayersTCP(new ServerMessageJoinedGame(newPlayer.data))
            res.send(new ServerMessagePositions())
        })
    }
}

export { AppServer }
