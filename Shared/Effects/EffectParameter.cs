using System;
using UnityEngine;

[Serializable]
public class EffectParameter
{
    [SerializeField]
    private string _parameterValue;
    [SerializeField]
    private string _typeName;
    [SerializeField]
    private string _parameterName;

    public object ToObject {
        get {
            Type type = Type.GetType(_typeName);

            if (type == typeof(int))
                return int.TryParse(_parameterValue, out int result) ? result : 0;
            else if (type == typeof(string))
                return _parameterValue;
            else if (type == typeof(bool))
                return bool.TryParse(_parameterValue, out bool result) && result;
            else if (type == typeof(float))
                return float.TryParse(_parameterValue, out float result) ? result : 0f;
            else if (type == typeof(GameObject))
                return Resources.Load<GameObject>($"{SharedConfig.Current.PrefabsPath}/{_parameterValue}");
            else if (type == typeof(Alteration))
                return Resources.Load<Alteration>($"{SharedConfig.Current.AlterationsPath}/{_parameterValue}");
            else if (type == typeof(StatisticType))
                return Enum.TryParse(_parameterValue, out StatisticType result) ? result : 0;
            else if (type == typeof(StateType))
                return Enum.TryParse(_parameterValue, out StateType result) ? result : 0;
            else if (type == typeof(DamageType))
                return Enum.TryParse(_parameterValue, out DamageType result) ? result : 0;
            return null;
        }
    }
    public string TypeName => _typeName;
    public string ParameterName => _parameterName;

    public EffectParameter(string typeName, string parameterName) {
        _typeName = typeName;
        _parameterName = parameterName;
    }
}
