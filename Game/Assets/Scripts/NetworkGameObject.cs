using System.Linq;
using UnityEngine;

public class NetworkGameObject : MonoBehaviour
{
    private const int _frequency = 20;
    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private bool _isMine;
    private Vector3 _destination;
    private string _id;
    private float _elapsedTime = 0f;

    private void OnServerMessagePosition(ServerMessagePositions serverMessagePositions) {
        if (serverMessagePositions.players.Any((ObjectData o) => o.id == _id))
            _destination = serverMessagePositions.players.First((ObjectData o) => o.id == _id).position.ToVector3();
    }

    private void OnServerMessageLeftGame(ServerMessageLeftGame serverMessageLeftGame) {
        if (serverMessageLeftGame.id == _id)
            Destroy(gameObject);
    }

    private void MoveTowardsDestination() {
        Vector3 direction = (_destination - transform.position).normalized;

        float distance = Vector3.Distance(transform.position, _destination);

        if (distance <= _movementSpeed * Time.deltaTime) {
            transform.position = _destination;
        } else {
            transform.position += direction * _movementSpeed * Time.deltaTime;
        }
    }

    private void SendPosition() {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= 1 / _frequency) {
            UDPClient.Send(new ClientMessagePosition(new Vector3Data(transform.position.x, transform.position.y, transform.position.z)));
            _elapsedTime = 0f;
        }
    }

    private void FixedUpdate() {
        if (!_isMine)
            MoveTowardsDestination();
    }

    private void Update() {
        if (_isMine)
            SendPosition();
    }

    private void Start() {
        MessageHandler.Current.onServerMessagePositions += OnServerMessagePosition;
        MessageHandler.Current.onServerMessageLeftGame += OnServerMessageLeftGame;
    }

    public string Id {
        get => _id;
        set => _id = value;
    }
}
