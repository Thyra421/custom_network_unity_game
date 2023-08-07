public struct PlayerMovementData
{
    public string id;
    public TransformData transform;
    public float timestamp;
    public PlayerAnimationData animation;

    public PlayerMovementData(string id, TransformData transform, float timestamp, PlayerAnimationData animation) {
        this.id = id;
        this.transform = transform;
        this.timestamp = timestamp;
        this.animation = animation;
    }
}