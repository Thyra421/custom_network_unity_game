using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterStatistics
{
    private readonly Character _character;
    private readonly StatisticController[] _statistics = new StatisticController[Enum.GetValues(typeof(StatisticType)).Length];

    public EventRegistry<StatisticType, float> Listeners { get; }
    public StatisticData[] Datas => _statistics.Select((StatisticController s) => s.Data).ToArray();

    private static StatisticData[] SelectDatas(List<StatisticController> statistics) => statistics.Select((StatisticController s) => s.Data).ToArray();

    public CharacterStatistics(Character character) {
        _character = character;
        for (int i = 0; i < Enum.GetValues(typeof(StatisticType)).Length; i++)
            _statistics[i] = new StatisticController((StatisticType)Enum.GetValues(typeof(StatisticType)).GetValue(i));
    }

    public StatisticController Find(StatisticType type) => Array.Find(_statistics, (StatisticController s) => s.Type == type);

    public void OnStatisticsChanged(List<StatisticController> modifiedStatistics) {
        if (_character is Player player)
            player.Send(new MessageStatisticsChanged(SelectDatas(modifiedStatistics)));
    }
}
