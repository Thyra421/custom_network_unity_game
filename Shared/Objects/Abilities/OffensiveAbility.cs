using UnityEngine;

public abstract class OffensiveAbility : Ability
{
    [SerializeField]
    private AbilityHit _hit;

    public AbilityHit Hit => _hit;
}