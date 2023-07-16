using System.Collections.Generic;
using UnityEngine;

class AttackHitbox : MonoBehaviour
{
    private Player _player;
    private readonly List<Collider> _collidersTouched = new List<Collider>();
    private int _damages;

    private void Start() {
        Destroy(gameObject, .5f);
    }

    private void OnTriggerEnter(Collider other) {
        if (_collidersTouched.Contains(other))
            return;
        Player otherPlayer = other.GetComponent<Player>();
        if (otherPlayer != null && otherPlayer != _player && otherPlayer.Room == _player.Room) {
            _collidersTouched.Add(other);
            otherPlayer.Statistics.CurrentHealth -= _damages;
            _player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(otherPlayer.Id, otherPlayer.Statistics.CurrentHealth, otherPlayer.Statistics.MaxHealth));
        }
    }

    public void Initialize(Player player, int damages) {
        _player = player;
        _damages = damages;
    }

    public Player Player => _player;
}