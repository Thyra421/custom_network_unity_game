using UnityEngine;

[RequireComponent(typeof(RemotePlayerMovement))]
[RequireComponent(typeof(RemotePlayerAttack))]
public class RemotePlayer : Player
{
    [SerializeField]
    private RemotePlayerMovement _movement;
    [SerializeField]
    private RemotePlayerAttack _attack;

    public RemotePlayerMovement Movement {
        get => _movement;
        set => _movement = value;
    }
    public RemotePlayerAttack Attack {
        get => _attack;
        set => _attack = value;
    }
}