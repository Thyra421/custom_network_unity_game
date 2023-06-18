using System.Collections.Generic;
using UnityEngine;

class AttackHitbox : MonoBehaviour
{
    private Avatar _avatar;
    private List<Collider> _collidersTouched = new List<Collider>();

    private void Start() {
        Destroy(gameObject, .5f);
    }

    private void OnTriggerEnter(Collider other) {
        if (_collidersTouched.Contains(other))
            return;
        Avatar otherAvatar = other.GetComponent<Avatar>();
        if (otherAvatar != _avatar) {
            _collidersTouched.Add(other);
            API.Players.BroadcastTCP(new MessageDamage(_avatar.Player.Data.id, otherAvatar.Player.Data.id));
        }
    }

    public Avatar Avatar {
        get => _avatar;
        set => _avatar = value;
    }
}