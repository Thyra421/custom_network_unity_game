public struct NPCData
{
    public string id;
    public TransformData transform;
    public NPCAnimationData animation;

    public NPCData(string id, TransformData transform, NPCAnimationData animation) {
        this.id = id;
        this.transform = transform;
        this.animation = animation;
    }
}