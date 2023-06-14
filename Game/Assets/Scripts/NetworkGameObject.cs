using System.Linq;
using UnityEngine;

public class NetworkGameObject : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 5f;
    private Vector3 _destination;
    private string _id;

    private void OnServerMessagePosition(ServerMessagePositions serverMessagePositions) {
        if (serverMessagePositions.players.Any((ObjectData o) => o.id == _id))
            _destination = serverMessagePositions.players.First((ObjectData o) => o.id == _id).position.ToVector3();
    }

    private void OnServerMessageLeftGame(ServerMessageLeftGame serverMessageLeftGame) {
        if (serverMessageLeftGame.id == _id)
            Destroy(gameObject);
    }

    private void MoveToDestination() {
        Vector3 direction = (_destination - transform.position).normalized;

        float distance = Vector3.Distance(transform.position, _destination);

        if (distance <= _movementSpeed * Time.deltaTime) {
            transform.position = _destination;
        } else {
            transform.position += direction * _movementSpeed * Time.deltaTime;
        }
    }

    private void Update() {
        MoveToDestination();
    }

    private void Start() {
        MessageHandler.Current.onServerMessagePositions += OnServerMessagePosition;
        MessageHandler.Current.onServerMessageLeftGame += OnServerMessageLeftGame;
    }

    public string Id {
        get => _id;
        set => _id = value;
    }

    //public static NetworkGameObject Find(string id) {
    //    NetworkGameObject[] networkGameObjects = FindObjectsOfType<NetworkGameObject>();
    //    if (!networkGameObjects.Any((NetworkGameObject g) => g.Id == id))
    //        return null;
    //    NetworkGameObject networkGameObject = networkGameObjects.First((NetworkGameObject g) => g.Id == id);
    //    return networkGameObject;
    //}
}
