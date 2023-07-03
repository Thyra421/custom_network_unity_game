using UnityEngine;

[RequireComponent (typeof(RoomNodesManager))]
[RequireComponent(typeof(RoomPlayersManager))]
[RequireComponent(typeof(RoomNPCsManager))]
public class Room : MonoBehaviour
{
    [SerializeField]
    private RoomNodesManager _nodesManager;
    [SerializeField]
    private RoomPlayersManager _playersManager;
    [SerializeField]
    private RoomNPCsManager _NPCsManager;

    public RoomNodesManager NodesManager => _nodesManager;

    public RoomPlayersManager PlayersManager => _playersManager;

    public RoomNPCsManager NPCsManager => _NPCsManager;
}
