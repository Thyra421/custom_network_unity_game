public class State
{
    private bool _value;

    public StateType Type { get; }
    public bool Value {
        get => _value;
        set {
            _value = value;
            OnStateChanged?.Invoke(_value);
        }
    }

    public delegate void OnStateChangedHandler(bool value);
    public event OnStateChangedHandler OnStateChanged;

    public State(StateType type) {
        Type = type;
    }
}