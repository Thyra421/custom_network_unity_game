using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private const int SERVER_PORT = 8080;
    private const string SERVER_ENDPOINT = "127.0.0.1";

    public static NetworkManager current;

    public readonly UDPClient udp = new UDPClient(SERVER_ENDPOINT, SERVER_PORT);
    public readonly TCPClient tcp = new TCPClient(SERVER_ENDPOINT, SERVER_PORT);
    public readonly HTTPClient http = new HTTPClient($"http://{SERVER_ENDPOINT}:{SERVER_PORT}");

    string id;

    public void SetId(string id) => this.id = id;
    public string GetId => id;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}