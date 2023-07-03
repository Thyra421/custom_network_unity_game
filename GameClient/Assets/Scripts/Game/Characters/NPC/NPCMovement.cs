using UnityEngine;

public class NPCMovement : Movement
{
    [SerializeField]
    protected float _rotationSpeed = 100f;
    private Vector3 _destinationPosition;
    private Vector3 _destinationRotation;
    private NPCAnimationData _NPCAnimationData;

    protected override void Move() {
        Vector3 direction = (_destinationPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, _destinationPosition);

        _animator.SetBool("IsRunning", _NPCAnimationData.isRunning);
        if (distance <= _movementSpeed * Time.deltaTime) {
            transform.position = _destinationPosition;
        } else {
            transform.position += _movementSpeed * Time.deltaTime * direction;
        }
    }

    protected override void Rotate() {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _destinationRotation, Time.deltaTime * _rotationSpeed);
    }

    private void Awake() {
        _movementSpeed = 2.5f;
        _animator = GetComponent<Animator>();
        _destinationPosition = transform.position;
        _destinationRotation = transform.eulerAngles;
    }

    public Vector3 DestinationPosition {
        get => _destinationPosition;
        set => _destinationPosition = value;
    }

    public Vector3 DestinationRotation {
        get => _destinationRotation;
        set => _destinationRotation = value;
    }

    public NPCAnimationData NPCAnimationData {
        get => _NPCAnimationData;
        set => _NPCAnimationData = value;
    }
}