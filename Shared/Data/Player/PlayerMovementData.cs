public struct PlayerMovementData
{
    public string id;
    public TransformData transformData;
    public PlayerAnimationData animationData;
    public float movementSpeed;

    public PlayerMovementData(string id, TransformData transformData, PlayerAnimationData animationData, float movementSpeed) {
        this.id = id;
        this.transformData = transformData;
        this.animationData = animationData;
        this.movementSpeed = movementSpeed;
    }
}