using UnityEngine;

public class CharacterAbilityActionController : MonoBehaviour, IAbilityActionController
{
    public void Melee(int damage, string animationName) {
    }

    public void Projectile(int damage, GameObject prefab, float speed, float distance) {
    }
}