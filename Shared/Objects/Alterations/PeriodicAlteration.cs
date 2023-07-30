using UnityEngine;

[CreateAssetMenu(menuName = "Alteration/Periodic")]
public class PeriodicAlteration : Alteration, IUsable
{
    [SerializeField]
    private DirectEffect[] _effects;
    [SerializeField]
    private float _intervalDurationInSeconds;

    public DirectEffect[] Effects => _effects;

    public float IntervalDurationInSeconds => _intervalDurationInSeconds;
}