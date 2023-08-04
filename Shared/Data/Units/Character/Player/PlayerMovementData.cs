public struct PlayerMovementData
{
    public string id;
    public TransformData transform;
    public float movementSpeed;
    public PlayerAnimationData animation;

    public PlayerMovementData(string id, TransformData transform, float movementSpeed, PlayerAnimationData animationData) {
        this.id = id;
        this.transform = transform;
        this.movementSpeed = movementSpeed;
        this.animation = animationData;
    }
}