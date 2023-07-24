using System.Collections.Generic;
using System.Linq;

public class PlayerStatusEffectController : IStatusEffectController
{
    private StatisticsData _statistics;

    public PlayerStatusEffectController(StatisticsData statistics) {
        _statistics = statistics;
    }

    public StatisticsData Apply(List<Alteration> alterations) {
        foreach (Alteration alteration in alterations)
            if (alteration is ContinuousAlteration continuousAlteration)
                foreach (StatusEffect effect in continuousAlteration.Effects)
                    typeof(PlayerStatusEffectController).GetMethod(effect.MethodName).Invoke(this, effect.Parameters.Select((EffectParameter param) => param.ToObject).ToArray());
        return _statistics;
    }

    public void Root() {
        _statistics._movementSpeed = 0;
    }

    public void Slow(float amount) {
        _statistics._movementSpeed -= amount;
    }
}