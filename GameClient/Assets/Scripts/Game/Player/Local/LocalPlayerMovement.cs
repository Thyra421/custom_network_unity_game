using System.Collections;
using UnityEngine;

public class LocalPlayerMovement : PlayerMovement
{
    private float _currentRotation = 0f;
    private bool _canTriggerAnimation = true;
    public float _cooldownDuration = 0.05f;
    private Vector3 _movement;

    protected override void Move() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        _movement = new Vector3(horizontalInput, 0f, verticalInput);

        if (_movement.magnitude > 0f) {
            _canTriggerAnimation = false;
            _movement.Normalize();
            _animator.SetFloat("X", _movement.x);
            _animator.SetFloat("Y", _movement.z);
            _animator.SetBool("IsRunning", true);

            transform.Translate(_movementSpeed * Time.deltaTime * _movement);
        } else if (!_canTriggerAnimation)
            StartCoroutine(StartCooldown());
        else
            _animator.SetBool("IsRunning", false);
    }

    protected override void Rotate() {
        //handled by camera       
    }

    private IEnumerator StartCooldown() {
        yield return new WaitForSeconds(_cooldownDuration);
        _canTriggerAnimation = true;
    }

    public Vector3 Movement {
        get => _movement;
        set => _movement = value;
    }
}