using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RemotePlayerMovement))]
public class RemotePlayer : NetworkObject
{
    [SerializeField]
    private RemotePlayerMovement _playerMovement;

    private void OnServerMessageMovements(ServerMessageMovements serverMessageMovements) {
        if (serverMessageMovements.players.Any((ObjectData o) => o.id == _id)) {
            ObjectData obj = serverMessageMovements.players.First((ObjectData o) => o.id == _id);
            _playerMovement.DestinationPosition = obj.transform.position.ToVector3();
            _playerMovement.DestinationRotation = obj.transform.rotation.ToVector3();
            _playerMovement.MovementData = obj.movement;
        }
    }

    private void OnServerMessageLeftGame(ServerMessageLeftGame serverMessageLeftGame) {
        if (serverMessageLeftGame.id == _id)
            Destroy(gameObject);
    }

    private void Start() {
        MessageHandler.Current.onServerMessageMovements += OnServerMessageMovements;
        MessageHandler.Current.onServerMessageLeftGame += OnServerMessageLeftGame;
    }
}