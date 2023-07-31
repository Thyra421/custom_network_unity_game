public class Statistic
{
    private float _value = 1;

    public StatisticType Type { get; }

    public delegate void OnStatisticChangedHandler(float value);
    public event OnStatisticChangedHandler OnStatisticChanged;

    public float Value {
        get => _value;
        set {
            _value = value;
            OnStatisticChanged(_value);
        }
    }

    public Statistic(StatisticType type) {
        Type = type;
    }
}