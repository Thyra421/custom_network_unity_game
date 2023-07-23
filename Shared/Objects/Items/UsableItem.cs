using UnityEngine;

[CreateAssetMenu(menuName = "Item/Usable")]
public class UsableItem : Item, IRechargeable, IUsable
{
    [SerializeField]
    private DirectEffect[] _effects;
    [SerializeField]
    private float _cooldown;

    public DirectEffect[] Effects => _effects;

    public float Cooldown => _cooldown;
}