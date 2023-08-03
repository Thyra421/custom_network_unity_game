public class UDPClient
{
    public string Address { get; }
    public int Port { get; }

    public MessageHandler MessageHandler { get; } = new MessageHandler();

    public UDPClient(string address, int port) {
        Address = address;
        Port = port;
    }

    public void Send<T>(T message) {
        API.UdpServer.Send(this, message);
    }
}
