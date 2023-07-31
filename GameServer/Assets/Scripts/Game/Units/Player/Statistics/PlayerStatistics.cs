using System;
using System.Linq;

public class PlayerStatistics
{
    private readonly Player _player;
    private readonly Statistic[] _statistics = new Statistic[Enum.GetValues(typeof(StatisticType)).Length];

    private StatisticData[] Datas => _statistics.Select((Statistic s) => s.Data).ToArray();

    private void OnStatisticsChanged(StatisticData[] oldStatistics) {
        StatisticData[] newStatistics = Datas;

        StatisticData[] difference = newStatistics.Where((StatisticData sd, int index) => sd.value != oldStatistics[index].value).ToArray();

        if (difference.Length > 0)
            _player.Client.TCP.Send(new MessageStatisticsChanged(difference));
    }

    public PlayerStatistics(Player player) {
        _player = player;
        for (int i = 0; i < Enum.GetValues(typeof(StatisticType)).Length; i++)
            _statistics[i] = new Statistic((StatisticType)Enum.GetValues(typeof(StatisticType)).GetValue(i));
    }

    public Statistic Find(StatisticType type) => Array.Find(_statistics, (Statistic s) => s.Type == type);

    public void OnAddedContinuousAlteration(ContinuousAlteration alteration) {
        StatisticData[] oldStatistics = Datas;
        new PlayerStatusEffectController(this).Add(alteration);

        OnStatisticsChanged(oldStatistics);
    }

    public void OnRemovedContinuousAlteration(ContinuousAlteration alteration) {
        StatisticData[] oldStatistics = Datas;
        new PlayerStatusEffectController(this).Remove(alteration);

        OnStatisticsChanged(oldStatistics);
    }
}
