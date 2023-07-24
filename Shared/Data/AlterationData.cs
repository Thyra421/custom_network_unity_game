public struct AlterationData
{
    public string targetId;
    public string ownerId;
    public string alterationName;
    public float remainingDuration;

    public AlterationData(string targetId, string ownerId, string alterationName, float remainingDuration) {
        this.targetId = targetId;
        this.ownerId = ownerId;
        this.alterationName = alterationName;
        this.remainingDuration = remainingDuration;
    }
}