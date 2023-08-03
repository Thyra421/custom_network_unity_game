using System;
using System.Collections.Generic;
using UnityEngine;

public class StatisticChangeListener
{
    internal OnStatisticChangedHandler OnStatisticChanged => OnStatisticChangedEvent;

    public event OnStatisticChangedHandler OnStatisticChangedEvent;
    public delegate void OnStatisticChangedHandler(float value);
}

public class StatisticsManager : MonoBehaviour
{
    private readonly Statistic[] _statistics = new Statistic[Enum.GetValues(typeof(StatisticType)).Length];

    public static StatisticsManager Current { get; private set; }
    public Dictionary<StatisticType, StatisticChangeListener> OnStatisticChanged { get; } = new Dictionary<StatisticType, StatisticChangeListener>();

    private void OnMessageStatisticsChanged(MessageStatisticsChanged messageStatisticsChanged) {
        foreach (StatisticData sd in messageStatisticsChanged.statisticDatas) {
            Find(sd.type).Value = sd.value;
            OnStatisticChanged[sd.type].OnStatisticChanged?.Invoke(sd.value);
        }
    }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);

        for (int i = 0; i < Enum.GetValues(typeof(StatisticType)).Length; i++) {
            StatisticType type = (StatisticType)Enum.GetValues(typeof(StatisticType)).GetValue(i);

            _statistics[i] = new Statistic(type);
            OnStatisticChanged.Add(type, new StatisticChangeListener());
        }

        TCPClient.MessageHandler.AddListener<MessageStatisticsChanged>(OnMessageStatisticsChanged);
    }

    public Statistic Find(StatisticType type) => Array.Find(_statistics, (Statistic s) => s.Type == type);
}