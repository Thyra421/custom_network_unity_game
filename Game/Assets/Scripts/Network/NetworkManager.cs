public class NetworkManager
{
    //private static NetworkManager _current;
    //private static readonly UDPClient _udp = new UDPClient();
    //private static readonly TCPClient _tcp = new TCPClient();
    //private static string _id;
    private static string _secret;

    //private void Awake() {
    //    if (_current == null) {
    //        _current = this;
    //        DontDestroyOnLoad(gameObject);
    //    } else
    //        Destroy(gameObject);
    //}

    //public static NetworkManager Current => _current;

    //public static string Id {
    //    get => _id;
    //    set => _id = value;
    //}

    public static string Secret {
        get => _secret;
        set => _secret = value;
    }

    //public static TCPClient Tcp => _tcp;

    //public static UDPClient Udp => _udp;
}