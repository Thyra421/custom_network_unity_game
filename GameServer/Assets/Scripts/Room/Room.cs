using UnityEngine;

[RequireComponent (typeof(NodesManager))]
[RequireComponent(typeof(PlayersManager))]
[RequireComponent(typeof(NPCsManager))]
[RequireComponent(typeof(VFXsManager))]
public class Room : MonoBehaviour
{
    [SerializeField]
    private NodesManager _nodesManager;
    [SerializeField]
    private PlayersManager _playersManager;
    [SerializeField]
    private NPCsManager _NPCsManager;
    [SerializeField]
    private VFXsManager _VFXsManager;

    public NodesManager NodesManager => _nodesManager;

    public PlayersManager PlayersManager => _playersManager;

    public NPCsManager NPCsManager => _NPCsManager;

    public VFXsManager VFXsManager => _VFXsManager;
}
