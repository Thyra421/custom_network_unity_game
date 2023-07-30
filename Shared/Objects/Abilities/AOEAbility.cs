using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AOE")]
public class AOEAbility : OffensiveAbility
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