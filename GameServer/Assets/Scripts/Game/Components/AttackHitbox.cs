using System.Collections.Generic;
using UnityEngine;

class AttackHitbox : MonoBehaviour
{
    private readonly List<Collider> _collidersTouched = new List<Collider>();
    private int _damages;
    private float _duration;
    private AbilityHit _hit;

    public Player Player { get; private set; }

    private void Start() {
        if (_duration > 0)
            Destroy(gameObject, _duration);
    }

    private void OnTriggerEnter(Collider other) {
        if (_collidersTouched.Contains(other))
            return;
        Player otherPlayer = other.GetComponent<Player>();
        if (otherPlayer != null && otherPlayer != Player && otherPlayer.Room == Player.Room) {
            _collidersTouched.Add(other);
            otherPlayer.EffectController.Use(_hit);
            otherPlayer.Statistics.CurrentHealth -= _damages;
            Player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(otherPlayer.Id, otherPlayer.Statistics.CurrentHealth, otherPlayer.Statistics.MaxHealth));
        }
    }

    public void Initialize(Player player, AbilityHit hit, float duration) {
        Player = player;
        _hit = hit;
        _duration = duration;
    }
}