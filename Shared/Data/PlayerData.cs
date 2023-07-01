public struct PlayerData
{
    public string id;
    public TransformData transform;
    public PlayerAnimationData animation;

    public PlayerData(string id, TransformData transform, PlayerAnimationData animation) {
        this.id = id;
        this.transform = transform;
        this.animation = animation;
    }
}