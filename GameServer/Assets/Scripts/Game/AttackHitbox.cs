using System.Collections.Generic;
using UnityEngine;

class AttackHitbox : MonoBehaviour
{
    private readonly List<Collider> _collidersTouched = new List<Collider>();
    private int _damages;

    public Player Player { get; private set; }

    private void Start() {
        Destroy(gameObject, .5f);
    }

    private void OnTriggerEnter(Collider other) {
        if (_collidersTouched.Contains(other))
            return;
        Player otherPlayer = other.GetComponent<Player>();
        if (otherPlayer != null && otherPlayer != Player && otherPlayer.Room == Player.Room) {
            _collidersTouched.Add(other);
            otherPlayer.Statistics.CurrentHealth -= _damages;
            Player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(otherPlayer.Id, otherPlayer.Statistics.CurrentHealth, otherPlayer.Statistics.MaxHealth));
        }
    }

    public void Initialize(Player player, int damages) {
        Player = player;
        _damages = damages;
    }
}