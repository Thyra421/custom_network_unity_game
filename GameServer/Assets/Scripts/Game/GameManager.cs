using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _current;
    [SerializeField]
    private List<Transform> _plainsSpawn;
    //[SerializeField]
    //private RawMaterial _commonMushroom;
    //[SerializeField]
    //private RawMaterial _rareMushroom;

    public static GameManager Current => _current;

    public Transform RandomPlainSpawn => _plainsSpawn[Random.Range(0, _plainsSpawn.Count)];

    //public RawMaterial CommonMushroom => _commonMushroom;

    //public RawMaterial RareMushroom => _rareMushroom;

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
