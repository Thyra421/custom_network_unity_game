using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _current;
    [SerializeField]
    private List<Transform> _mushroomSpawns;

    public static GameManager Current => _current;

    public List<Transform> MushroomSpawns => _mushroomSpawns;

    public Transform RandomMusroomSpawn => _mushroomSpawns[Random.Range(0, _mushroomSpawns.Count)];

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
