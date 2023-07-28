using UnityEngine;

[CreateAssetMenu(menuName = "Config/Config")]
public class Config : SingletonScriptableObject<Config>
{
    [SerializeField]
    private int _serverPortTCP = 8080;
    [SerializeField]
    private int _serverPortUDP = 8080;
    [SerializeField]
    private int _serverPortHTTP = 80;
    [SerializeField]
    private string _serverAddress = "127.0.0.1";

    public int ServerPortTCP => _serverPortTCP;

    public int ServerPortUDP => _serverPortUDP;

    public int ServerPortHTTP => _serverPortHTTP;

    public string ServerAddress => _serverAddress;
}
