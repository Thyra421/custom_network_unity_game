using System.Net.Sockets;
using System.Text;

namespace TestUDP;

public class TCPClient
{
    private NetworkStream _stream;

    private void OnDisconnect() {
        Console.WriteLine($"[TCPClient] disconnected");

        if (API.Players.Any(this)) {
            Player player = API.Players.Find(this)!;
            API.Players.Remove(this);
            API.Players.BroadcastTCP(new ServerMessageLeftGame(player.Data.id), player);
        }
        API.Clients.Remove(this);
    }

    private void OnMessage(string message) {
        Console.WriteLine($"[TCPClient] received {message}");

        ClientMessage clientMessage = Utils.ParseJsonString<ClientMessage>(message);

        switch (clientMessage.type) {
            case ClientMessageType.authenticate:
                ClientMessageAuthenticate clientMessageAuthenticate = Utils.ParseJsonString<ClientMessageAuthenticate>(message);
                OnMessageAuthenticate(clientMessageAuthenticate);
                break;
        }
    }

    private void OnMessageAuthenticate(ClientMessageAuthenticate clientMessageAuthenticate) {
        Client client = API.Clients.Find(this);
        client.Authenticate(new UDPClient(clientMessageAuthenticate.udpAddress, clientMessageAuthenticate.udpPort), clientMessageAuthenticate.secret);
    }

    public TCPClient(TcpClient tcpClient) {
        _stream = tcpClient.GetStream();
        Listen();
    }

    public async Task Send(ServerMessage message) {
        byte[] msg = Encoding.ASCII.GetBytes(Utils.ObjectToString(message));
        await _stream.WriteAsync(msg, 0, msg.Length);
    }

    public async void Listen() {
        byte[] bytes = new byte[256];
        int i;

        try {
            while ((i = await _stream.ReadAsync(bytes, 0, bytes.Length)) != 0) {
                string message = Encoding.ASCII.GetString(bytes, 0, i);
                OnMessage(message);
            }
            OnDisconnect();
        } catch (Exception) {
            OnDisconnect();
        }
    }
}
