public struct PlayerData
{
    public string id;
    public TransformData transformData;

    public PlayerData(string id, TransformData transformData) {
        this.id = id;
        this.transformData = transformData;
    }
}