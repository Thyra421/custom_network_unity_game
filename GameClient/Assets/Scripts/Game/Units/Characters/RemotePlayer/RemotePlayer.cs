using UnityEngine;

[RequireComponent(typeof(RemoteUnitMovement))]
public class RemotePlayer : Character
{
    [SerializeField]
    private RemotePlayerAnimation _animation;
    [SerializeField]
    private RemoteUnitMovement _movement;

    public override CharacterAnimation CharacterAnimation => _animation;
    public RemoteUnitMovement Movement => _movement;
    public RemotePlayerAnimation Animation => _animation;
}