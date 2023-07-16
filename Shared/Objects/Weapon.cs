using UnityEngine;

[CreateAssetMenu]
public class Weapon : Item
{
    [SerializeField]
    private Ability[] _abilities;

    public Ability[] Abilities => _abilities;
}