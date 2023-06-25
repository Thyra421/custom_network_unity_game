using System.Collections.Generic;
using UnityEngine;

class Reception : MonoBehaviour
{
    private static Reception _current;
    [SerializeField]
    private GameObject _roomPrefab;
    private readonly List<Room> _rooms = new List<Room>();

    private Player JoinRoom(Client client, Room room) {
        Player newPlayer = room.PlayersManager.CreatePlayer(client);
        return newPlayer;
    }

    private Player CreateRoom(Client client) {
        Room newRoom = Instantiate(_roomPrefab).GetComponent<Room>();
        _rooms.Add(newRoom);
        Player newPlayer = newRoom.PlayersManager.CreatePlayer(client);
        return newPlayer;
    }

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
    }

    public void RemoveRoom(Room room) {
        _rooms.Remove(room);
        Destroy(room.gameObject);
    }

    public Player JoinOrCreateRoom(Client client) {
        Room room = _rooms.Find((Room r) => !r.PlayersManager.IsFull);
        if (room == null)
            return CreateRoom(client);
        return JoinRoom(client, room);
    }

    public static Reception Current => _current;
}