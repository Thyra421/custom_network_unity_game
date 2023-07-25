using TMPro;
using UnityEngine;

public class StatisticGUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _nameText;
    [SerializeField]
    private TMP_Text _valueText;
    private Statistic _statistic;

    private void OnStatisticChanged(float value) {
        _valueText.text = value.ToString();
    }

    public void Initialize(Statistic statistic) {
        _statistic = statistic;
        _nameText.text = statistic.Type.ToString();
        _statistic.OnStatisticChanged += OnStatisticChanged;
    }
}