using UnityEngine;

[RequireComponent(typeof(RemotePlayerMovement))]
[RequireComponent(typeof(RemotePlayerAttack))]
[RequireComponent(typeof(RemotePlayerStatistics))]
public class RemotePlayer : Player
{
    [SerializeField]
    private RemotePlayerMovement _movement;
    [SerializeField]
    private RemotePlayerAttack _attack;
    [SerializeField]
    private RemotePlayerStatistics _statistics;

    public void Initialize(PlayerData data) {
        _id = data.id;
    }

    public RemotePlayerMovement Movement {
        get => _movement;
        set => _movement = value;
    }
    public RemotePlayerAttack Attack => _attack;

    public override PlayerStatistics Statistics => _statistics;
}