using System;
using UnityEngine;

[Serializable]
public class AbilityHit : IUsable
{
    [SerializeField]
    private Effect[] _effects;

    public Effect[] Effects => _effects;
}
