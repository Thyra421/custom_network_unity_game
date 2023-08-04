public enum CharacterType
{
    Player,
    NPC
}

public struct CharacterData
{
    public string id;
    public CharacterType type;

    public CharacterData(string id, CharacterType type) {
        this.id = id;
        this.type = type;
    }
}