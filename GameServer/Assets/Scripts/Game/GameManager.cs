using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _current;
    [SerializeField]
    private GameObject _avatarPrefab;

    public static GameManager Current {
        get => _current;
        set => _current = value;
    }

    public void DestroyAvatar(Transform avatar) {
        Destroy(avatar.gameObject);
    }

    public Transform CreateAvatar() {
        return Instantiate(_avatarPrefab).transform;
    }

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
