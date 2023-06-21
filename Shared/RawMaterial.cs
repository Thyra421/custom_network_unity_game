using UnityEngine;

public enum RawMaterialRegion
{
    Plains
}

[CreateAssetMenu(menuName = "Raw Material")]
public class RawMaterial : Item
{
    [SerializeField]
    private RawMaterialRegion _region;

    public RawMaterialRegion Region => _region;
}