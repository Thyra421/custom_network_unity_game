using System.Collections.Generic;
using UnityEngine;

class Reception : MonoBehaviour
{
    private static Reception _current;
    [SerializeField]
    private GameObject _roomPrefab;
    private List<Room> _rooms = new List<Room>();

    private Player JoinRoom(Client client, Room room) {
        Player newPlayer = room.CreatePlayer(client);
        return newPlayer;
    }

    private Player CreateRoom(Client client) {
        Room newRoom = Instantiate(_roomPrefab, transform).GetComponent<Room>();
        _rooms.Add(newRoom);
        Player newPlayer = newRoom.CreatePlayer(client);
        return newPlayer;
    }

    private void Awake() {
        if (_current == null) {
            _current = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void Remove(Room room) {
        Destroy(room.gameObject);
        _rooms.Remove(room);
    }

    public Player JoinOrCreateRoom(Client client) {
        Room room = _rooms.Find((Room r) => !r.IsFull);
        if (room == null)
            return CreateRoom(client);
        return JoinRoom(client, room);
    }

    public static Reception Current => _current;
}