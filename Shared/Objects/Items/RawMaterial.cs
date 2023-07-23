using UnityEngine;

public enum RawMaterialRegion
{
    Plains
}

[CreateAssetMenu(menuName = "Item/RawMaterial")]
public class RawMaterial : Item
{
    [SerializeField]
    private RawMaterialRegion _region;

    public RawMaterialRegion Region => _region;
}