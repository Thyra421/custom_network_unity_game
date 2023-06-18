using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField]
    private GameObject _attackHitbox;
    private Player _player;

    public Player Player {
        get => _player;
        set => _player = value;
    }

    public void Attack() {
        AttackHitbox attackHitbox = Instantiate(_attackHitbox, transform).GetComponent<AttackHitbox>();
        attackHitbox.Avatar = this;
    }
}
