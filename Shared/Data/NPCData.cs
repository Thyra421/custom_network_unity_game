public struct NPCData
{
    public string id;
    public TransformData transformData;
    public NPCAnimationData NPCAnimationData;
    public string animalName;

    public NPCData(string id, TransformData transformData, NPCAnimationData NPCAnimationData, string animalName) {
        this.id = id;
        this.transformData = transformData;
        this.NPCAnimationData = NPCAnimationData;
        this.animalName = animalName;
    }
}