using System;
using UnityEngine;

[Serializable]
public class ActionParameter
{
    [SerializeField]
    private string _parameterValue;
    [SerializeField]
    private string _typeName;
    [SerializeField]
    private string _parameterName;

    public ActionParameter(string typeName, string parameterName) {
        _typeName = typeName;
        _parameterName = parameterName;
    }

    public string TypeName => _typeName;

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
            }
            return null;
        }
    }
}

[Serializable]
public abstract class Action
{
    [SerializeField]
    private string _methodName;
    [SerializeField]
    private ActionParameter[] _parameters;
    private readonly string _classAssemblyQualifiedName;


    public Action(string classAssemblyQualifiedName) {
        _classAssemblyQualifiedName = classAssemblyQualifiedName;
    }

    public string MethodName => _methodName;

    public ActionParameter[] Parameters => _parameters;

    public string ClassName => _classAssemblyQualifiedName;
}