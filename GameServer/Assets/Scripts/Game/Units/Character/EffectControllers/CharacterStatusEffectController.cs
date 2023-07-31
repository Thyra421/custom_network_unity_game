using System.Linq;

public class CharacterStatusEffectController : IStatusEffectController
{
    private readonly CharacterStatistics _statistics;

    public CharacterStatusEffectController(CharacterStatistics statistics) {
        _statistics = statistics;
    }

    public void Add(ContinuousAlteration alteration) {
        foreach (StatusEffect effect in alteration.Effects)
            typeof(CharacterStatusEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void Remove(ContinuousAlteration alteration) {
        foreach (StatusEffect effect in alteration.Effects)
            typeof(CharacterStatusEffectController).GetMethod($"Remove{effect.MethodName}").Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
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