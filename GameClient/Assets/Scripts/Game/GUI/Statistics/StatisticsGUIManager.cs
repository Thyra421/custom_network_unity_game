using System;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsGUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private Transform _parent;
    private readonly List<StatisticGUI> _statisticGUIs = new List<StatisticGUI>();

    private void Start() {
        foreach (StatisticType st in Enum.GetValues(typeof(StatisticType))) {
            StatisticGUI newStatisticGUI = Instantiate(_prefab, _parent).GetComponent<StatisticGUI>();
            newStatisticGUI.Initialize(StatisticsManager.Current.Find(st));
            _statisticGUIs.Add(newStatisticGUI);
        }
    }
}