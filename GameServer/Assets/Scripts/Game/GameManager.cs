using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _current;
    [SerializeField]
    private Transform[] _plainsSpawn;

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
    }

    private void Start() {
        API.Start();
    }

    public Transform RandomPlainSpawn => _plainsSpawn[Random.Range(0, _plainsSpawn.Length)];

    public static GameManager Current => _current;

}
