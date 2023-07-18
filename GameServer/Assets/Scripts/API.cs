public static class API
{
    private static readonly TCPServer _tcpServer = new TCPServer();

    public static ClientsManager Clients { get; } = new ClientsManager();
    public static UDPServer UdpServer { get; } = new UDPServer();

    public static void Start() {
        _tcpServer.Start();
        UdpServer.Start();
    }
}