using System.Collections.Generic;
using UnityEngine;

class PersistentAbilityHitbox : MonoBehaviour
{
    private float _duration;
    private Alteration _alteration;
    private List<Player> _playersHit = new List<Player>();

    public Player Player { get; private set; }

    private void Start() {
        if (_duration > 0)
            Destroy(gameObject, _duration);
    }

    private void OnDestroy() {
        if (_playersHit.Count > 0)
            _playersHit.ForEach((Player p) => p.Alterations.Remove(_alteration, Player));
    }

    private void OnTriggerEnter(Collider other) {
        Player otherPlayer = other.GetComponent<Player>();

        if (otherPlayer != null && otherPlayer != Player && otherPlayer.Room == Player.Room) {
            _playersHit.Add(otherPlayer);
            otherPlayer.Alterations.Apply(_alteration, Player);
        }
    }

    private void OnTriggerExit(Collider other) {
        Player otherPlayer = other.GetComponent<Player>();

        if (otherPlayer != null && otherPlayer != Player && otherPlayer.Room == Player.Room) {
            otherPlayer.Alterations.Remove(_alteration, Player);
            _playersHit.Remove(otherPlayer);
        }
    }

    public void Initialize(Player player, Alteration alteration, float duration) {
        Player = player;
        _alteration = alteration;
        _duration = duration;
    }
}