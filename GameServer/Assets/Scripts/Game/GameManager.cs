using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _current;
    [SerializeField]
    private List<Transform> _plainsSpawn;

    public static GameManager Current => _current;

    public Transform RandomPlainSpawn => _plainsSpawn[Random.Range(0, _plainsSpawn.Count)];

    private void Awake() {
        if (_current == null)
            _current = this;
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        API.Start();
    }
}
