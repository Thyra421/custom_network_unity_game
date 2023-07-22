using UnityEngine;

[CreateAssetMenu]
public class UsableItem : Item, IRechargeable, IUsable
{
    [SerializeField]
    private Effect[] _effects;
    [SerializeField]
    private float _cooldown;

    public Effect[] Effects => _effects;

    public float Cooldown => _cooldown;
}