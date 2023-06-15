using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RemotePlayerMovement))]
public class RemotePlayer : NetworkObject
{
    [SerializeField]
    private RemotePlayerMovement _playerMovement;

    private void OnServerMessageMovements(ServerMessageMovements serverMessageMovements) {
        if (serverMessageMovements.players.Any((ObjectData o) => o.id == _id)) {
            TransformData transformData = serverMessageMovements.players.First((ObjectData o) => o.id == _id).transform;
            _playerMovement.Destination = transformData.position.ToVector3();
            _playerMovement.transform.eulerAngles = transformData.rotation.ToVector3();
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