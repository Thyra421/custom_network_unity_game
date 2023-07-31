public abstract class CharacterMovement
{
    protected abstract void Move();

    protected abstract void Rotate();

    public virtual void FixedUpdate() {
        Move();
        Rotate();
    }
}