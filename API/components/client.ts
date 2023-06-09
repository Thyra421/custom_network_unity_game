import { TCPClient } from "./tcp_client";
import { UDPClient } from "./udp_client";

class Client {
    readonly id: string
    tcp: TCPClient
    udp: UDPClient

    constructor(id: string) {
        this.id = id
    }
}

export { Client }