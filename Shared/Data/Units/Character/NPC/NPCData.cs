public struct NPCData
{
    public string id;
    public TransformData transform;
    public string animalName;

    public NPCData(string id, TransformData transform,  string animalName) {
        this.id = id;
        this.transform = transform;
        this.animalName = animalName;
    }
}