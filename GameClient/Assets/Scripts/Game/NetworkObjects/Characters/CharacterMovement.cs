using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
    protected abstract void Move();

    protected abstract void Rotate();

    public virtual void FixedUpdate() {
        Move();
        Rotate();
    }
}