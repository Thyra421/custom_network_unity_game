static class API
{
    private static readonly ClientsManager _clients = new ClientsManager();
    private static readonly TCPServer _tcpServer = new TCPServer();
    private static readonly UDPServer _udpServer = new UDPServer();

    public static void Start() {
        _tcpServer.Start();
        _udpServer.Start();
    }

    public static ClientsManager Clients => _clients;

    public static TCPServer TcpServer => _tcpServer;

    public static UDPServer UdpServer => _udpServer;
}