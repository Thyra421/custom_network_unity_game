using System;
using System.Linq;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private int _maxHealth;
    private int _currentHealth;

    private readonly Statistic[] _statistics = new Statistic[Enum.GetValues(typeof(StatisticType)).Length];

    private void Awake() {
        for (int i = 0; i < Enum.GetValues(typeof(StatisticType)).Length; i++)
            _statistics[i] = new Statistic((StatisticType)Enum.GetValues(typeof(StatisticType)).GetValue(i));
    }

    private StatisticData[] Datas => _statistics.Select((Statistic s) => s.Data).ToArray();

    private void OnStatisticsChanged(StatisticData[] oldStatistics) {
        StatisticData[] newStatistics = Datas;

        StatisticData[] difference = newStatistics.Where((StatisticData sd, int index) => sd.value != oldStatistics[index].value).ToArray();

        if (difference.Length > 0)
            _player.Client.Tcp.Send(new MessageStatisticsChanged(difference));
    }

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

    public int CurrentHealth {
        get => _currentHealth;
        set {
            _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
            _player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(_player.Id, _player.Statistics.CurrentHealth, _player.Statistics.MaxHealth));
        }
    }

    public int MaxHealth {
        get => _maxHealth;
        set {
            _maxHealth = value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            _player.Room.PlayersManager.BroadcastTCP(new MessageHealthChanged(_player.Id, _player.Statistics.CurrentHealth, _player.Statistics.MaxHealth));
        }
    }

    public Statistic Find(StatisticType type) => Array.Find(_statistics, (Statistic s) => s.Type == type);
}
