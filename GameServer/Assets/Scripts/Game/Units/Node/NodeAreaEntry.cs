using System;
using UnityEngine;

[Serializable]
public class NodeAreaEntry
{
    [SerializeField]
    private int _amount;
    [SerializeField]
    private DropSource _dropSource;

    public int Amount => _amount;
    public DropSource DropSource => _dropSource;
}
