using System;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    private readonly Statistic[] _statistics = new Statistic[Enum.GetValues(typeof(StatisticType)).Length];

    public static StatisticsManager Current { get; private set; }

    private void OnMessageStatisticsChanged(MessageStatisticsChanged messageStatisticsChanged) {
        foreach (StatisticData sd in messageStatisticsChanged.statisticDatas)
            Find(sd.type).Value = sd.value;
    }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);

        for (int i = 0; i < Enum.GetValues(typeof(StatisticType)).Length; i++)
            _statistics[i] = new Statistic((StatisticType)Enum.GetValues(typeof(StatisticType)).GetValue(i));

        MessageHandler.Current.OnMessageStatisticsChangedEvent += OnMessageStatisticsChanged;
    }

    public Statistic Find(StatisticType type) => Array.Find(_statistics, (Statistic s) => s.Type == type);
}