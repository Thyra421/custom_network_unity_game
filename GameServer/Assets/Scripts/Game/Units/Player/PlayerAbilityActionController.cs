using System.Linq;
using UnityEngine;

public class PlayerAbilityActionController : IAbilityActionController
{
    private readonly Player _player;

    public PlayerAbilityActionController(Player player) {
        _player = player;
    }

    public void Use(Ability ability) {
        foreach (AbilityAction action in ability.Actions) {
            typeof(IAbilityActionController).GetMethod(action.MethodName).Invoke(this, action.Parameters.Select((ActionParameter param) => param.ToObject).ToArray());
        }
    }

    public void Melee(int damage, string animationName) {
        AttackHitbox attackHitbox = Object.Instantiate(_player.Abilities.MeleePrefab, _player.transform).GetComponent<AttackHitbox>();
        attackHitbox.Initialize(_player, damage);
    }

    public void Projectile(int damage, GameObject prefab, float speed, float distance) {
        throw new System.NotImplementedException();
    }
}