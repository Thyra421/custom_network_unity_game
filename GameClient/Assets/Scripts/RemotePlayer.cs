using UnityEngine;

[RequireComponent(typeof(RemotePlayerMovement))]
public class RemotePlayer : NetworkObject
{
    [SerializeField]
    private RemotePlayerMovement _playerMovement;

    public RemotePlayerMovement PlayerMovement {
        get => _playerMovement;
        set => _playerMovement = value;
    }
}