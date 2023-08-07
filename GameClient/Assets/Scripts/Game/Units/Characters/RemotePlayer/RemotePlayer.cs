using UnityEngine;

[RequireComponent(typeof(RemoteCharacterMovement))]
public class RemotePlayer : Character
{
    [SerializeField]
    private RemotePlayerAnimation _animation;
    [SerializeField]
    private RemoteCharacterMovement _movement;

    public override CharacterAnimation CharacterAnimation => _animation;
    public RemoteCharacterMovement Movement => _movement;
    public RemotePlayerAnimation Animation => _animation;
}