using UnityEngine;

public class LocalPlayerAttack : PlayerAttack
{
    public override void Attack() {
        _animator.SetTrigger("Attack");
        TCPClient.Send(new MessageAttack());
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            Attack();
    }
}
