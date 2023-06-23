public class RemotePlayerAttack : PlayerAttack
{
    public override void Attack() {
        _animator.SetTrigger("Attack");
    }
}
