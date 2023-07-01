using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField]
    protected float _movementSpeed = 7f;    
    [SerializeField]
    protected Animator _animator;

    protected abstract void Move();

    protected abstract void Rotate();

    private void FixedUpdate() {
        Move();
        Rotate();
    }
}