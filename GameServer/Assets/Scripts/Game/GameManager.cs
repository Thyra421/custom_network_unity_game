using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _current;
    [SerializeField]
    private NPCArea[] _NPCAreas;
    [SerializeField]
    private GameObject _playerTemplate;
    [SerializeField]
    private NodeArea[] _nodeAreas;
    public Text debugText;

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
    }

    private void Start() {
        API.Start();
    }

    public static GameManager Current => _current;

    public NPCArea[] NPCAreas => _NPCAreas;

    public NodeArea[] NodeAreas => _nodeAreas;

    public GameObject PlayerTemplate => _playerTemplate;
}
