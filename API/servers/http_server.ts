import { Server, createServer } from 'http'
import { SERVER_PORT } from '../config'
import express from 'express'

class HTTPServer {
    readonly httpServer: Server

    constructor(appServer: express.Application) {
        this.httpServer = createServer(appServer)
    }

    start = () => {
        this.httpServer.listen(SERVER_PORT)
        console.log(`[HTTP server] listening on port ${SERVER_PORT}`)
    }
}


export { HTTPServer }