using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private NPCArea[] _NPCAreas;
    [SerializeField]
    private NodeArea[] _nodeAreas;
    public Text debugText;

    public NPCArea[] NPCAreas => _NPCAreas;
    public NodeArea[] NodeAreas => _nodeAreas;

    private void Start() {
        API.Start();
    }
}
