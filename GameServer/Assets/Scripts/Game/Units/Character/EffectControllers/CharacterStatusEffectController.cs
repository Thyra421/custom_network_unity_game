using System.Collections.Generic;
using System.Linq;

public class CharacterPersistentEffectController : IPersistentEffectController
{
    private readonly Character _character;
    private List<StatisticController> _modifiedStatistics;
    private List<StateController> _modifiedStates;

    public CharacterPersistentEffectController(Character character) {
        _character = character;
    }

    public void Add(ContinuousAlteration alteration, List<StatisticController> modifiedStatistics, List<StateController> modifiedStates) {
        _modifiedStatistics = modifiedStatistics;
        _modifiedStates = modifiedStates;

        foreach (StatusEffect effect in alteration.Effects)
            typeof(CharacterPersistentEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void Remove(ContinuousAlteration alteration, List<StatisticController> modifiedStatistics, List<StateController> modifiedStates) {
        _modifiedStatistics = modifiedStatistics;
        _modifiedStates = modifiedStates;

        foreach (StatusEffect effect in alteration.Effects)
            typeof(CharacterPersistentEffectController).GetMethod($"Remove{effect.MethodName}").Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
    }

    public void ModifyStatistic(StatisticType type, float value, bool percent) {
        StatisticController statistic = _character.Statistics.Find(type);
        statistic.Modifiers.Add(new StatisticModifier(value, percent));
        _modifiedStatistics.Add(statistic);
    }

    public void RemoveModifyStatistic(StatisticType type, float value, bool percent) {
        StatisticController statistic = _character.Statistics.Find(type);
        statistic.Modifiers.Remove(new StatisticModifier(value, percent));
        _modifiedStatistics.Add(statistic);
    }

    public void StateStack(StateType type) {
        StateController state = _character.States.Find(type);
        bool initialValue = state.Value;
        state.Counter++;
        if (state.Value != initialValue)
            _modifiedStates.Add(state);
    }

    public void RemoveStateStack(StateType type) {
        StateController state = _character.States.Find(type);
        bool initialValue = state.Value;
        state.Counter--;
        if (state.Value != initialValue)
            _modifiedStates.Add(state);
    }
}