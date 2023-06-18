using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    protected Animator _animator;

    public abstract void Attack();
}
