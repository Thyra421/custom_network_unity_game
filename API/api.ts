import { UDPServer } from './servers/udp_server'
import { TCPServer } from './servers/tcp_server'
import { AppServer } from './servers/app_server'
import { HTTPServer } from './servers/http_server'
import { Clients } from "./components/clients"
import { Players } from './components/players'

const appServer: AppServer = new AppServer()
const httpServer: HTTPServer = new HTTPServer(appServer.appServer)
const tcpServer: TCPServer = new TCPServer(httpServer.httpServer)
const udpServer: UDPServer = new UDPServer()

const clients: Clients = new Clients()
const players: Players = new Players()

appServer.start()
httpServer.start()
tcpServer.start()
udpServer.start()

export { udpServer, clients, players }