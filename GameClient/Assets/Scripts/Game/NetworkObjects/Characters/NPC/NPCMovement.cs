using UnityEngine;

public class NPCMovement : Movement
{
    protected float _rotationSpeed = 100f;

    public Vector3 DestinationPosition { get; set; }
    public Vector3 DestinationRotation { get; set; }
    public NPCAnimationData NPCAnimationData { get; set; }

    protected override void Move() {
        Vector3 direction = (DestinationPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, DestinationPosition);

        _animator.SetBool("IsRunning", NPCAnimationData.isRunning);
        if (distance <= _movementSpeed * Time.deltaTime) {
            transform.position = DestinationPosition;
        } else {
            transform.position += _movementSpeed * Time.deltaTime * direction;
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

    public void Initialize(Animal animal) {
        _movementSpeed = animal.MovementSpeed;
    }
}