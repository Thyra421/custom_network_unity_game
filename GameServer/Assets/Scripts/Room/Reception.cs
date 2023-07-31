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

        client.TCP.Send(new MessageGameState(newPlayer.Id, newPlayer.Room.PlayersManager.Datas));
        client.TCP.Send(new MessageSpawnNodes(newPlayer.Room.NodesManager.Datas));
        client.TCP.Send(new MessageSpawnNPCs(newPlayer.Room.NPCsManager.Datas));

        return newPlayer;
    }

    private Player CreateAndJoinRoom(Client client) {
        Room newRoom = Instantiate(_roomPrefab).GetComponent<Room>();
        _rooms.Add(newRoom);
        return JoinRoom(client, newRoom);
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
            return CreateAndJoinRoom(client);
        return JoinRoom(client, room);
    }
}