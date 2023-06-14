namespace TestUDP;

static class API
{
    private static readonly Clients _clients = new Clients();
    private static readonly TCPServer _tcpServer = new TCPServer();
    private static readonly UDPServer _udpServer = new UDPServer();
    private static readonly HTTPServer _httpServer = new HTTPServer();
    private static readonly Players _players = new Players();

    private static void Main(string[] args) {
        _httpServer.Start();
        _tcpServer.Start();
        _udpServer.Start();
        _players.StartSyncing();

        while (Console.ReadKey(true).KeyChar != 'q') {
            Console.WriteLine("Press q to quit.");
        }
    }

    public static Clients Clients => _clients;

    public static TCPServer TcpServer => _tcpServer;

    public static UDPServer UdpServer => _udpServer;

    public static HTTPServer HttpServer => _httpServer;

    public static Players Players => _players;
}