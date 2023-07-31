using UnityEngine;

public abstract class Effect
{
    [SerializeField]
    private string _methodName;
    [SerializeField]
    private EffectParameter[] _parameters;

    public string ClassName { get; }
    public string MethodName => _methodName;
    public EffectParameter[] Parameters => _parameters;

    public Effect(string className) {
        ClassName = className;
    }    
}
