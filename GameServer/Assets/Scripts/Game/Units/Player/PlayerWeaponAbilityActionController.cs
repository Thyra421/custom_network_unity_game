using System.Linq;
using UnityEngine;

public class PlayerWeaponAbilityActionController : WeaponAbilityActionController
{
    private readonly Player _player;

    public PlayerWeaponAbilityActionController(Player player) {
        _player = player;
    }

    public void Use(WeaponAbility ability) {
        foreach (WeaponAbilityAction action in ability.Actions) {
            typeof(WeaponAbilityActionController).GetMethod(action.MethodName).Invoke(this, action.Parameters.Select((ActionParameter param) => param.ToObject).ToArray());
        }
    }

    public override void Melee(string animationName) {
        AttackHitbox attackHitbox = Object.Instantiate(_player.Attack.MeleePrefab, _player.transform).GetComponent<AttackHitbox>();
        attackHitbox.Initialize(_player);

    }

    public override void Projectile(GameObject prefab, float speed, float distance) {
        throw new System.NotImplementedException();
    }
}