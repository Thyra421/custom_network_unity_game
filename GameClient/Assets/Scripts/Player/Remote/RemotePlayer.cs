using UnityEngine;

[RequireComponent(typeof(RemotePlayerMovement))]
[RequireComponent(typeof(RemotePlayerAttack))]
public class RemotePlayer : Player
{
    [SerializeField]
    private RemotePlayerMovement _movement;
    [SerializeField]
    private RemotePlayerAttack _attack;

    private void Start() {
        if (_movement == null)
            _movement = GetComponent<RemotePlayerMovement>();
        if (_attack == null)
            _attack = GetComponent<RemotePlayerAttack>();
    }

    public RemotePlayerMovement Movement {
        get => _movement;
        set => _movement = value;
    }
    public RemotePlayerAttack Attack {
        get => _attack;
        set => _attack = value;
    }
}