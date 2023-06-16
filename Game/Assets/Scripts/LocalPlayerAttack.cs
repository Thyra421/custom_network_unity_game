using UnityEngine;

public class LocalPlayerAttack : Attack
{
    private void Attack() {
        _animator.SetTrigger("Attack");
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0))
            Attack();
    }
}
