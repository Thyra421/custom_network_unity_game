using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    public void Attack() {
        AttackHitbox attackHitbox = Instantiate(Resources.Load<GameObject>($"{Config.PREFABS_PATH}/AttackHitbox"), transform).GetComponent<AttackHitbox>();
        attackHitbox.Initialize(_player);
    }
}