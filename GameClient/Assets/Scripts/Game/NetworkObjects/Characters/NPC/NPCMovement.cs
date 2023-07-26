using UnityEngine;

public class NPCMovement : Movement
{
    protected float _rotationSpeed = 100f;

    public Vector3 DestinationPosition { get; set; }
    public Vector3 DestinationRotation { get; set; }
    public NPCAnimationData NPCAnimationData { get; set; }
    public float MovementSpeed { get; set; }

    protected override void Move() {
        Vector3 direction = (DestinationPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, DestinationPosition);

        _animator.SetBool("IsRunning", NPCAnimationData.isRunning);
        if (distance <= MovementSpeed * Time.deltaTime) {
            transform.position = DestinationPosition;
        } else {
            transform.position += MovementSpeed * Time.deltaTime * direction;
        }
    }

    protected override void Rotate() {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, DestinationRotation, Time.deltaTime * _rotationSpeed);
    }

    private void Awake() {
        _animator = GetComponent<Animator>();
        DestinationPosition = transform.position;
        DestinationRotation = transform.eulerAngles;
    }
}