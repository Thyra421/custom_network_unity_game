using System.Collections.Generic;
using UnityEngine;

class DirectAbilityHitbox : MonoBehaviour
{
    private readonly List<Collider> _collidersTouched = new List<Collider>();
    private float _duration;
    private AbilityHit _hit;

    public Character Character { get; private set; }

    private void Start() {
        if (_duration > 0)
            Destroy(gameObject, _duration);
    }

    private void OnTriggerEnter(Collider other) {
        if (_collidersTouched.Contains(other))
            return;
        Character otherCharacter = other.GetComponent<Character>();

        if (otherCharacter != null && otherCharacter != Character && otherCharacter.Room == Character.Room) {
            _collidersTouched.Add(other);
            new CharacterDirectEffectController(otherCharacter, Character).Use(_hit);
        }
    }

    public void Initialize(Character player, AbilityHit hit, float duration) {
        Character = player;
        _hit = hit;
        _duration = duration;
    }
}