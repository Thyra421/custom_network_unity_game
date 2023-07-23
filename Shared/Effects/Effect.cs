using UnityEngine;

public abstract class Effect
{
    [SerializeField]
    private string _methodName;
    [SerializeField]
    private EffectParameter[] _parameters;

    public string ClassName { get; }

    public Effect(string className) {
        ClassName = className;
    }

    public string MethodName => _methodName;

    public EffectParameter[] Parameters => _parameters;
}
