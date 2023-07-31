public struct AlterationData
{
    public CharacterData target;
    public CharacterData owner;
    public string alterationName;
    public float remainingDuration;

    public AlterationData(CharacterData target, CharacterData owner, string alterationName, float remainingDuration) {
        this.target = target;
        this.owner = owner;
        this.alterationName = alterationName;
        this.remainingDuration = remainingDuration;
    }
}