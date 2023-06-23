using UnityEngine;

public abstract class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    protected float _movementSpeed = 5f;
    [SerializeField]
    protected float _rotationSpeed = 100f;
    [SerializeField]
    protected Animator _animator;

    protected abstract void Move();

    protected abstract void Rotate();

    private void FixedUpdate() {
        Move();
        Rotate();
    }
}