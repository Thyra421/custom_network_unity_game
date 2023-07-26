using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField]
    protected Animator _animator;

    protected abstract void Move();

    protected abstract void Rotate();

    private void FixedUpdate() {
        Move();
        Rotate();
    }
}