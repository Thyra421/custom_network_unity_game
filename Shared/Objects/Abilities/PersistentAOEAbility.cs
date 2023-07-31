using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AOE/Persistent")]
public class PersistentAOEAbility : Ability
{
    [SerializeField]
    private Alteration _alteration;
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private float _delayInSeconds;
    [SerializeField]
    private float _durationInSeconds;

    public GameObject Prefab => _prefab;
    public float DelayInSeconds => _delayInSeconds;
    public float DurationInSeconds => _durationInSeconds;
    public Alteration Alteration => _alteration;
}