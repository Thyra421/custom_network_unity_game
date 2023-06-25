public class UDPClient
{
    readonly string _address;
    readonly int _port;

    public UDPClient(string address, int port) {
        _address = address;
        _port = port;
    }

    public string Address => _address;

    public int Port => _port;

    public void Send<T>(T message) {
        API.UdpServer.Send(this, message);
    }
}
