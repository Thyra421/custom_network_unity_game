using UnityEngine;

[CreateAssetMenu(menuName = "Config/Config")]
public class Config : SingletonScriptableObject<Config>
{
    [Header("Network")]
    [SerializeField]
    private int _TCPPort = 8080;
    [SerializeField]
    private int _UDPPort = 8080;
    [SerializeField]
    private string _address = "127.0.0.1";
    [Header("Gameplay")]
    [SerializeField]
    private float _levelExperienceIncreaseMultiplicator = 1.3f;
    [SerializeField]
    private int _maxPlayersPerRoom = 2;
    [SerializeField]
    private GameObject _meleePrefab;
    [SerializeField]
    private GameObject _dashPrefab;

    public int TCPPort => _TCPPort;
    public int UDPPort => _UDPPort;
    public string Address => _address;
    public float LevelExperienceIncreaseMultiplicator => _levelExperienceIncreaseMultiplicator;
    public int MaxPlayersPerRoom => _maxPlayersPerRoom;
    public GameObject MeleePrefab => _meleePrefab;
    public GameObject DashPrefab => _dashPrefab;
}