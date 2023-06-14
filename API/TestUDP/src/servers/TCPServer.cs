using System.Net;
using System.Net.Sockets;

namespace TestUDP;

class TCPServer
{
    private TcpListener _server;

    private async void Listen() {
        if (_server == null) {
            OnError();
            return;
        }
        while (true) {
            TcpClient tcpClient = await _server.AcceptTcpClientAsync();
            OnConnect(tcpClient);
        }
    }

    private static void OnConnect(TcpClient tcpClient) {
        Console.WriteLine("[TCPServer] client connected");

        API.Clients.Create(new TCPClient(tcpClient));
    }

    private static void OnStarted() {
        Console.WriteLine("[TCPServer] started");
    }

    private void OnListening() {
        Console.WriteLine($"[TCPServer] listening on {_server.LocalEndpoint}");
    }

    private static void OnError() {
        Console.WriteLine($"[TCPServer] not connected");
    }

    private static void OnConnectionFailed() {
        Console.WriteLine($"[TCPServer] connection failed");
    }

    public void Start() {
        _server = new TcpListener(IPAddress.Parse(Config.Address), Config.TCPPort);
        _server.Start();
        OnStarted();
        Listen();
        OnListening();
    }
}