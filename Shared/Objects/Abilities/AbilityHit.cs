using System;
using UnityEngine;

[Serializable]
public class AbilityHit : IUsable
{
    [SerializeField]
    private bool _pierce;
    [SerializeField]
    private DirectEffect[] _effects;

    public bool Pierce => _pierce;
    public DirectEffect[] Effects => _effects;
}
