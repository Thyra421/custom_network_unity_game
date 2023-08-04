public class UDPClient
{
    public string Address { get; }
    public int Port { get; }
    public MessageHandler MessageHandler { get; } = new MessageHandler();

    public UDPClient(string address, int port) {
        Address = address;
        Port = port;
    }

    /// <summary>
    /// Prepares the message and sends it.
    /// Use SendBytes for better performance when broadcasting the same message to several clients to avoid re-serializing it each time.
    /// </summary>
    public void Send<T>(T message) {
        API.UdpServer.Send(this, message);
    }

    public void SendBytes(byte[] bytes) {
        API.UdpServer.SendBytes(this, bytes);
    }
}
