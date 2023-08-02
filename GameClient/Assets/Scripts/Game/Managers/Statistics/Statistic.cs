public class Statistic
{
    private float _value = 1;

    public StatisticType Type { get; }
    public float Value {
        get => _value;
        set {
            _value = value;
            OnStatisticChanged?.Invoke(_value);
        }
    }

    public delegate void OnStatisticChangedHandler(float value);
    public event OnStatisticChangedHandler OnStatisticChanged;

    public Statistic(StatisticType type) {
        Type = type;
    }
}