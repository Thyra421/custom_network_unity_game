public struct NPCMovementData
{
    public string id;
    public TransformData transformData;
    public NPCAnimationData NPCAnimationData;
    public float speed;

    public NPCMovementData(string id, TransformData transformData, NPCAnimationData NPCAnimationData, float speed) {
        this.id = id;
        this.transformData = transformData;
        this.NPCAnimationData = NPCAnimationData;
        this.speed = speed;
    }
}