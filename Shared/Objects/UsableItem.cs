using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class OnUseEntryParameter
{
    [SerializeField]
    private string _parameterValue;
    [SerializeField]
    private string _typeName;
    [SerializeField]
    private string _parameterName;

    public OnUseEntryParameter(string typeName, string parameterName) {
        _typeName = typeName;
        _parameterName = parameterName;
    }

    public string TypeName => _typeName;

    public object ToObject {
        get {
            Type type = Type.GetType(_typeName);

            if (type == typeof(int)) {
                int.TryParse(_parameterValue, out int result);
                return result;
            } else if (type == typeof(string))
                return _parameterValue;
            else if (type == typeof(bool)) {
                bool.TryParse(_parameterValue, out bool result);
                return result;
            }
            return null;
        }
    }
}

[Serializable]
public class OnUseEntry
{
    [SerializeField]
    private string _methodName;
    [SerializeField]
    private OnUseEntryParameter[] _parameters;

    public string MethodName => _methodName;

    public OnUseEntryParameter[] Parameters => _parameters;
}

[CreateAssetMenu]
public class UsableItem : Item
{
    [SerializeField]
    private OnUseEntry[] _onUses;

    public void Use(Player player) {
        OnUses onUses = new OnUses(player);

        foreach (OnUseEntry entry in _onUses) {
            typeof(OnUses).GetMethod(entry.MethodName).Invoke(onUses, entry.Parameters.Select((OnUseEntryParameter param) => param.ToObject).ToArray());
        }
    }
}