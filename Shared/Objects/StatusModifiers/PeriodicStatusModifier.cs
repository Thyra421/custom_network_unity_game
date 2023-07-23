using UnityEngine;

[CreateAssetMenu(menuName = "StatusModifier/Periodic")]
public class PeriodicStatusModifier : StatusModifier, IUsable
{
    [SerializeField]
    private DirectEffect[] _effects;
    [SerializeField]
    private float _intervalDuration;

    public DirectEffect[] Effects => _effects;

    public float IntervalDuration => _intervalDuration;
}