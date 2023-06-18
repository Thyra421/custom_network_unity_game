using UnityEngine;

public class LocalPlayerAttack : PlayerAttack
{
    public override void Attack() {
        _animator.SetTrigger("Attack");
        TCPClient.Send(new ClientMessageAttack());
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0))
            Attack();
    }
}
