public struct PlayerData
{
    public string id;
    public TransformData transform;
    public AnimationData animation;

    public PlayerData(string id, TransformData transform, AnimationData animation) {
        this.id = id;
        this.transform = transform;
        this.animation = animation;
    }
}