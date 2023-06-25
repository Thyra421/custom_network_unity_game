using UnityEngine;

[RequireComponent (typeof(RoomNodesManager))]
[RequireComponent(typeof(RoomPlayersManager))]
public class Room : MonoBehaviour
{
    [SerializeField]
    private RoomNodesManager _nodesManager;
    [SerializeField]
    private RoomPlayersManager _playersManager;

    public RoomNodesManager NodesManager => _nodesManager;

    public RoomPlayersManager PlayersManager => _playersManager;
}
