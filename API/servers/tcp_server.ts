import { WebSocketServer, WebSocket, RawData } from "ws";
import { Server } from 'http'
import { TCPClient } from "../components/tcp_client";
import { ClientMessage, ClientMessageAuthenticate, ClientMessageType } from "../data/client_message";
import { clients } from "../api";

class TCPServer {
    readonly tcpServer: WebSocketServer

    constructor(httpServer: Server) {
        this.tcpServer = new WebSocketServer({ server: httpServer })
    }

    start = () => {
        this.tcpServer.on('connection', (socket: WebSocket) => {
            socket.on("close", this.onClose)
            socket.on('message', message => { this.onMessage(message, socket) })
        })
        console.log(`[TCP server] running`)
    }

    private onClose = (reason: Buffer) => {

    }

    private onMessage = (msg: RawData, socket: WebSocket) => {
        const jsonMessage = JSON.parse(msg.toString())
        console.log("[TCP message] " + msg)
        const message: ClientMessage = jsonMessage

        switch (message.type) {
            case ClientMessageType.authenticate:
                this.onMessageAuthenticate(message, socket)
                break
        }

    }
    onMessageAuthenticate = (messageAuthenticate: ClientMessageAuthenticate, socket: WebSocket): void => {
        clients.findFromId(messageAuthenticate.id).tcp = new TCPClient(socket)
    }
}


export { TCPServer }
