using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _current;
    [SerializeField]
    private Transform[] _plainsSpawn;
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

    public Transform FindSpawn(Vector3 spawnPost) => Array.Find(_plainsSpawn, (Transform t) => t.position == spawnPost);

    public Transform RandomPlainSpawn => _plainsSpawn[UnityEngine.Random.Range(0, _plainsSpawn.Length)];

    public static GameManager Current => _current;

}
