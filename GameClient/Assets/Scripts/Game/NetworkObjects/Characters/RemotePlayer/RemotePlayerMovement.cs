using UnityEngine;

public class RemotePlayerMovement : Movement
{
    [SerializeField]
    protected float _rotationSpeed = 100f;

    public Vector3 DestinationPosition { get; set; }
    public Vector3 DestinationRotation { get; set; }
    public PlayerAnimationData PlayerAnimationData { get; set; }
    public float MovementSpeed { get; set; }

    protected override void Move() {
        Vector3 direction = (DestinationPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, DestinationPosition);

        _animator.SetBool("IsRunning", PlayerAnimationData.isRunning);
        _animator.SetBool("IsGrounded", PlayerAnimationData.isGrounded);
        _animator.SetFloat("X", PlayerAnimationData.x);
        _animator.SetFloat("Y", PlayerAnimationData.y);
        if (distance <= MovementSpeed * Time.deltaTime || distance > 2) {
            transform.position = DestinationPosition;
        } else {
            transform.position += MovementSpeed * Time.deltaTime * direction;
        }
    }

    protected override void Rotate() {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, DestinationRotation, Time.deltaTime * _rotationSpeed);
    }

    private void Awake() {
        DestinationPosition = transform.position;
        DestinationRotation = transform.eulerAngles;
    }
}