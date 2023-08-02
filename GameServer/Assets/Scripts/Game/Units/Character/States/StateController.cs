public class StateController
{
    public StateType Type { get; private set; }
    public float Counter { get; set; }
    public bool Value => Counter > 0;
    public StateData Data => new StateData(Type, Value);

    public StateController(StateType type) {
        Type = type;
    }
}
