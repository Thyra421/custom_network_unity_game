using System;
using System.Linq;

public class CharacterStatistics
{
    private readonly Character _character;
    private readonly Statistic[] _statistics = new Statistic[Enum.GetValues(typeof(StatisticType)).Length];

    private StatisticData[] Datas => _statistics.Select((Statistic s) => s.Data).ToArray();

    private void OnStatisticsChanged(StatisticData[] oldStatistics) {
        StatisticData[] newStatistics = Datas;

        StatisticData[] difference = newStatistics.Where((StatisticData sd, int index) => sd.value != oldStatistics[index].value).ToArray();

        if (_character is Player player && difference.Length > 0)
            player.Client.TCP.Send(new MessageStatisticsChanged(difference));
    }

    public CharacterStatistics(Character character) {
        _character = character;
        for (int i = 0; i < Enum.GetValues(typeof(StatisticType)).Length; i++)
            _statistics[i] = new Statistic((StatisticType)Enum.GetValues(typeof(StatisticType)).GetValue(i));
    }

    public Statistic Find(StatisticType type) => Array.Find(_statistics, (Statistic s) => s.Type == type);

    public void OnAddedContinuousAlteration(ContinuousAlteration alteration) {
        StatisticData[] oldStatistics = Datas;
        new CharacterStatusEffectController(this).Add(alteration);

        OnStatisticsChanged(oldStatistics);
    }

    public void OnRemovedContinuousAlteration(ContinuousAlteration alteration) {
        StatisticData[] oldStatistics = Datas;
        new CharacterStatusEffectController(this).Remove(alteration);

        OnStatisticsChanged(oldStatistics);
    }
}
