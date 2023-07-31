public struct NPCMovementData
{
    public string id;
    public TransformData transformData;
    public NPCAnimationData NPCAnimationData;
    public float movementSpeed;

    public NPCMovementData(string id, TransformData transformData, NPCAnimationData NPCAnimationData, float movementSpeed) {
        this.id = id;
        this.transformData = transformData;
        this.NPCAnimationData = NPCAnimationData;
        this.movementSpeed = movementSpeed;
    }
}