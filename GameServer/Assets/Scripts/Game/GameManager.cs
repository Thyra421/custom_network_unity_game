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

    public void DestroyAvatar(Avatar avatar) {
        Destroy(avatar.gameObject);
    }

    public Avatar CreateAvatar() {
        return Instantiate(_avatarPrefab).GetComponent<Avatar>();
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
