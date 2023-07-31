using UnityEngine;

[RequireComponent(typeof(RemotePlayerMovement))]
public class RemotePlayer : Character
{
    [SerializeField]
    private RemotePlayerAnimation _animation;
    [SerializeField]
    private RemotePlayerMovement _movement;

    public RemotePlayerMovement Movement => _movement;
    public RemotePlayerAnimation Animation => _animation;
}