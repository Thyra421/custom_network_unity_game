using System.Collections.Generic;
using System.Linq;

public class CharacterStatusEffectController : IStatusEffectController
{
    private readonly Character _character;
    private List<Statistic> _modifiedStatistics;

    public CharacterStatusEffectController(Character character) {
        _character = character;
    }

    public void Add(ContinuousAlteration alteration, List<Statistic> modifiedStatistics) {
        _modifiedStatistics = modifiedStatistics;
        foreach (StatusEffect effect in alteration.Effects)
            typeof(CharacterStatusEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void Remove(ContinuousAlteration alteration, List<Statistic> modifiedStatistics) {
        _modifiedStatistics = modifiedStatistics;
        foreach (StatusEffect effect in alteration.Effects)
            typeof(CharacterStatusEffectController).GetMethod($"Remove{effect.MethodName}").Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void ModifyStatistic(StatisticType type, float value, bool percent) {
        Statistic statistic = _character.Statistics.Find(type);
        statistic.Modifiers.Add(new StatisticModifier(value, percent));
        _modifiedStatistics.Add(statistic);
    }

    public void RemoveModifyStatistic(StatisticType type, float value, bool percent) {
        Statistic statistic = _character.Statistics.Find(type);
        statistic.Modifiers.Remove(new StatisticModifier(value, percent));
        _modifiedStatistics.Add(statistic);
    }

    public void Root() {
    }

    public void RemoveRoot() {
    }
}