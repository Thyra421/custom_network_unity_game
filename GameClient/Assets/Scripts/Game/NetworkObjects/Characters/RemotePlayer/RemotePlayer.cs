using UnityEngine;

[RequireComponent(typeof(RemotePlayerMovement))]
public class RemotePlayer : Character
{
    [SerializeField]
    private RemotePlayerMovement _movement;

    public RemotePlayerMovement Movement => _movement;
}