public class NetworkManager
{
    private static string _secret;

    public static string Secret {
        get => _secret;
        set => _secret = value;
    }
}