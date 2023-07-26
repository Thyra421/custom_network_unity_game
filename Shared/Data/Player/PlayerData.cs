public struct PlayerData
{
    public string id;
    public TransformData transformData;
    public PlayerAnimationData animationData;

    public PlayerData(string id, TransformData transformData, PlayerAnimationData animationData) {
        this.id = id;
        this.transformData = transformData;
        this.animationData = animationData;
    }
}