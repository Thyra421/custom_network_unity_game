public enum StateType
{
    Rooted, Stunned, Immune
}

public struct StateData
{
    public StateType type;
    public bool value;

    public StateData(StateType type, bool value) {
        this.type = type;
        this.value = value;
    }
}