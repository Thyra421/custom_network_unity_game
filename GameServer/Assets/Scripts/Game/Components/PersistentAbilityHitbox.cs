using System.Collections.Generic;
using UnityEngine;

class PersistentAbilityHitbox : MonoBehaviour
{
    private float _duration;
    private Alteration _alteration;
    private readonly List<Character> _playersHit = new List<Character>();

    public Character Character { get; private set; }

    private void Start() {
        if (_duration > 0)
            Destroy(gameObject, _duration);
    }    

    private void OnTriggerEnter(Collider other) {
        Character otherCharacter = other.GetComponent<Character>();

        if (otherCharacter != null && otherCharacter != Character && otherCharacter.Room == Character.Room) {
            _playersHit.Add(otherCharacter);
            otherCharacter.Alterations.Apply(_alteration, Character);
        }
    }

    private void OnTriggerExit(Collider other) {
        Character otherCharacter = other.GetComponent<Character>();

        if (otherCharacter != null && otherCharacter != Character && otherCharacter.Room == Character.Room) {
            otherCharacter.Alterations.Remove(_alteration, Character);
            _playersHit.Remove(otherCharacter);
        }
    }

    private void OnDestroy() {
        if (_playersHit.Count > 0)
            _playersHit.ForEach((Character p) => p.Alterations.Remove(_alteration, Character));
    }

    public void Initialize(Character player, Alteration alteration, float duration) {
        Character = player;
        _alteration = alteration;
        _duration = duration;
    }
}