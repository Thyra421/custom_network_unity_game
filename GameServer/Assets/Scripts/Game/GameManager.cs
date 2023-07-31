using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private NPCArea[] _NPCAreas;
    [SerializeField]
    private GameObject _playerTemplate;
    [SerializeField]
    private NodeArea[] _nodeAreas;
    public Text debugText;

    public static GameManager Current { get; private set; }
    public NPCArea[] NPCAreas => _NPCAreas;
    public NodeArea[] NodeAreas => _nodeAreas;
    public GameObject PlayerTemplate => _playerTemplate;

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
    }

    private void Start() {
        API.Start();
    }    
}
