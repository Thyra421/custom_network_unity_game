using System.Linq;

public class PlayerStatusEffectController : IStatusEffectController
{
    private PlayerStatistics _statistics;

    public PlayerStatusEffectController(PlayerStatistics statistics) {
        _statistics = statistics;
    }

    public void Add(ContinuousAlteration alteration) {
        foreach (StatusEffect effect in alteration.Effects)
            typeof(PlayerStatusEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void Remove(ContinuousAlteration alteration) {
        foreach (StatusEffect effect in alteration.Effects)
            typeof(PlayerStatusEffectController).GetMethod($"Remove{effect.MethodName}").Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void ModifyStatistic(StatisticType type, float value, bool percent) {
        _statistics.Find(type).Modifiers.Add(new StatisticModifier(value, percent));
    }

    public void RemoveModifyStatistic(StatisticType type, float value, bool percent) {
        _statistics.Find(type).Modifiers.Remove(new StatisticModifier(value, percent));
    }

    public void Root() {
    }

    public void RemoveRoot() {
    }
}