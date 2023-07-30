using UnityEngine;

public abstract class DirectAbility : Ability
{
    [SerializeField]
    private AbilityHit _hit;

    public AbilityHit Hit => _hit;
}
