using UnityEngine;

[RequireComponent(typeof(RemotePlayerMovement))]
[RequireComponent(typeof(RemotePlayerAttack))]
public class RemotePlayer : Player
{
    [SerializeField]
    private RemotePlayerMovement _movement;
    [SerializeField]
    private RemotePlayerAttack _attack;

    public void Initialize(PlayerData data) {
        _id = data.id;
    }

    public RemotePlayerMovement Movement {
        get => _movement;
        set => _movement = value;
    }
    public RemotePlayerAttack Attack => _attack;
}