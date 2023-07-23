using UnityEngine;

[CreateAssetMenu(menuName = "Alteration/Periodic")]
public class PeriodicAlteration : Alteration, IUsable
{
    [SerializeField]
    private DirectEffect[] _effects;
    [SerializeField]
    private float _intervalDuration;

    public DirectEffect[] Effects => _effects;

    public float IntervalDuration => _intervalDuration;
}