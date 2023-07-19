using System.Linq;
using UnityEngine;

public class CharacterAbilityEffectController : IAbilityEffectController
{
    private readonly Character _character;

    public CharacterAbilityEffectController(Character character) {
        _character = character;
    }

    public void Use(Ability ability) {
        foreach (AbilityEffect effect in ability.Effects)
            typeof(CharacterAbilityEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void Melee(int damage, string animationName, float duration) {
        _character.TriggerAnimation(animationName);
    }

    public void Projectile(int damage, GameObject prefab, float speed, float distance) {
    }
}