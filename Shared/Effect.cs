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

    public EffectParameter(string typeName, string parameterName) {
        _typeName = typeName;
        _parameterName = parameterName;
    }

    public object ToObject {
        get {
            Type type = Type.GetType(_typeName);

            if (type == typeof(int)) {
                bool success = int.TryParse(_parameterValue, out int result);
                return success ? result : 0;
            } else if (type == typeof(string))
                return _parameterValue;
            else if (type == typeof(bool)) {
                bool success = bool.TryParse(_parameterValue, out bool result);
                return success && result;
            } else if (type == typeof(float)) {
                bool success = float.TryParse(_parameterValue, out float result);
                return success ? result : 0f;
            } else if (type == typeof(GameObject)) {
                GameObject result = Resources.Load<GameObject>($"{SharedConfig.PREFABS_PATH}/{_parameterValue}");
                return result;
            }
            return null;
        }
    }

    public string TypeName => _typeName;

    public string ParameterName => _parameterName;
}

[Serializable]
public abstract class Effect
{
    [SerializeField]
    private string _methodName;
    [SerializeField]
    private EffectParameter[] _parameters;

    public string ClassAssemblyQualifiedName { get; }

    public Effect(string classAssemblyQualifiedName) {
        ClassAssemblyQualifiedName = classAssemblyQualifiedName;
    }

    public string MethodName => _methodName;

    public EffectParameter[] Parameters => _parameters;
}