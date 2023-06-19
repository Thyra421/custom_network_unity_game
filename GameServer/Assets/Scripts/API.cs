static class API
{
    private static readonly Clients _clients = new Clients();
    private static readonly TCPServer _tcpServer = new TCPServer();
    private static readonly UDPServer _udpServer = new UDPServer();

    public static void Start() {
        _tcpServer.Start();
        _udpServer.Start();
    }

    public static Clients Clients => _clients;

    public static TCPServer TcpServer => _tcpServer;

    public static UDPServer UdpServer => _udpServer;
}