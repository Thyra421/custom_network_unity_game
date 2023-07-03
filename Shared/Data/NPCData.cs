public struct NPCData
{
    public string id;
    public TransformData transform;
    public NPCAnimationData animation;
    public string prefabName;

    public NPCData(string id, TransformData transform, NPCAnimationData animation, string prefabName) {
        this.id = id;
        this.transform = transform;
        this.animation = animation;
        this.prefabName = prefabName;
    }
}