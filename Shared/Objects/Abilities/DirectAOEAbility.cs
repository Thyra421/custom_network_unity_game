using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AOE/Direct")]
public class DirectAOEAbility : DirectAbility
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private float _delayInSeconds;
    [SerializeField]
    private float _durationInSeconds;

    public GameObject Prefab => _prefab;

    public float DelayInSeconds => _delayInSeconds;

    public float DurationInSeconds => _durationInSeconds;
}