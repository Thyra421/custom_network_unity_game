using UnityEngine;

public class NPCMovement : CharacterMovement
{
    private Vector3 _destinationPosition;
    private Vector3 _destinationRotation;
    private float _movementSpeed;

    private NPC NPC { get; set; }

    protected override void Move() {
        Vector3 direction = (_destinationPosition - NPC.transform.position).normalized;
        float distance = Vector3.Distance(NPC.transform.position, _destinationPosition);

        if (distance <= _movementSpeed * Time.deltaTime)
            NPC.transform.position = _destinationPosition;
        else
            NPC.transform.position += _movementSpeed * Time.deltaTime * direction;
    }

    protected override void Rotate() {
        NPC.transform.eulerAngles = _destinationRotation;
    }

    public NPCMovement(NPC npc) {
        NPC = npc;
        _destinationPosition = NPC.transform.position;
        _destinationRotation = NPC.transform.eulerAngles;
    }

    public void SetMovement(TransformData transformData, float movementSpeed) {
        _destinationPosition = transformData.position.ToVector3;
        _destinationRotation = transformData.rotation.ToVector3;
        _movementSpeed = movementSpeed;
    }
}