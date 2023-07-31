using UnityEngine;

[CreateAssetMenu]
public class Animal : ScriptableObject
{
    [SerializeField]
    private string _displayName;
    [SerializeField]
    private float _respawnTimerInSeconds;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private bool _mobile;
    [SerializeField]
    private float _movementSpeed;

    public float RespawnTimerInSeconds => _respawnTimerInSeconds;
    public GameObject Prefab => _prefab;
    public bool Mobile => _mobile;
    public string DisplayName => _displayName;
    public float MovementSpeed => _movementSpeed;
}