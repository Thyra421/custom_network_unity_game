using UnityEngine;

[CreateAssetMenu(menuName = "StatusModifier/Continuous")]
public class ContinuousStatusModifier : StatusModifier
{
    [SerializeField]
    private StatusEffect[] _effects;

    public StatusEffect[] Effects => _effects;
}