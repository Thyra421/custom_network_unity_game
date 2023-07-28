using UnityEngine;

[CreateAssetMenu(menuName = "Config/Config")]
public class Config : SingletonScriptableObject<Config>
{
    [SerializeField]
    private int _TCPPort = 8080;
    [SerializeField]
    private int _UDPPort = 8080;
    [SerializeField]
    private string _address = "127.0.0.1";
    [SerializeField]
    private float _levelExperienceIncreaseMultiplicator = 1.3f;
    [SerializeField]
    private int _maxPlayersPerRoom = 2;

    public int TCPPort => _TCPPort;

    public int UDPPort => _UDPPort;

    public string Address => _address;

    public float LevelExperienceIncreaseMultiplicator => _levelExperienceIncreaseMultiplicator;

    public int MaxPlayersPerRoom => _maxPlayersPerRoom;
}