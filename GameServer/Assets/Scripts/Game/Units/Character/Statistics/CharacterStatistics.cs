using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterStatistics
{
    private readonly Character _character;
    private readonly Statistic[] _statistics = new Statistic[Enum.GetValues(typeof(StatisticType)).Length];

    public StatisticData[] Datas => _statistics.Select((Statistic s) => s.Data).ToArray();

    private static StatisticData[] SelectDatas(List<Statistic> statistics) => statistics.Select((Statistic s) => s.Data).ToArray();

    public CharacterStatistics(Character character) {
        _character = character;
        for (int i = 0; i < Enum.GetValues(typeof(StatisticType)).Length; i++)
            _statistics[i] = new Statistic((StatisticType)Enum.GetValues(typeof(StatisticType)).GetValue(i));
    }

    public Statistic Find(StatisticType type) => Array.Find(_statistics, (Statistic s) => s.Type == type);

    public void OnStatisticsChanged(List<Statistic> modifiedStatistics) {
        if (_character is Player player)
            player.Client.TCP.Send(new MessageStatisticsChanged(SelectDatas(modifiedStatistics)));
    }
}
