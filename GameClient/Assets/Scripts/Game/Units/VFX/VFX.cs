using UnityEngine;

[RequireComponent(typeof(RemoteUnitMovement))]
public class VFX : Unit
{
    public RemoteUnitMovement Movement { get; private set; }

    private void Awake() {
        Movement = GetComponent<RemoteUnitMovement>();
    }
}