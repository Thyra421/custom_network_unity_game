using System.Collections.Generic;
using UnityEngine;

class Reception : MonoBehaviour
{
    [SerializeField]
    private GameObject _roomPrefab;
    private readonly List<Room> _rooms = new List<Room>();

    public static Reception Current { get; private set; }

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
        if (Current == null)
            Current = this;
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
}