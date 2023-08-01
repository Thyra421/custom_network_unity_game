using System.Collections.Generic;
using UnityEngine;

class DirectAbilityHitbox : MonoBehaviour
{
    private readonly List<Collider> _collidersTouched = new List<Collider>();
    private float _duration;
    private AbilityHit _hit;

    public Character Owner { get; private set; }

    private void Start() {
        if (_duration > 0)
            Destroy(gameObject, _duration);
    }

    private void OnTriggerEnter(Collider other) {
        if (_collidersTouched.Contains(other))
            return;

        Character target = other.GetComponent<Character>();

        if (target != null && target != Owner && target.Room == Owner.Room) {
            _collidersTouched.Add(other);
            target.DirectEffectController.Use(_hit, Owner);
        }
    }

    public void Initialize(Character owner, AbilityHit hit, float duration) {
        Owner = owner;
        _hit = hit;
        _duration = duration;
    }
}