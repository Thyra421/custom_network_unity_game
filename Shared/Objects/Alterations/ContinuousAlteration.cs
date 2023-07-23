using UnityEngine;

[CreateAssetMenu(menuName = "Alteration/Continuous")]
public class ContinuousAlteration : Alteration
{
    [SerializeField]
    private StatusEffect[] _effects;

    public StatusEffect[] Effects => _effects;
}