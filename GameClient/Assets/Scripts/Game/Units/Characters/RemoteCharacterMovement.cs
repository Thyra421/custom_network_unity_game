using UnityEngine;

public class RemoteCharacterMovement : RemoteUnitMovement
{
    [SerializeField]
    private Character _character;

    private bool IsGrounded => Physics.CheckSphere(transform.position, .1f, Config.Current.WhisIsGround);
    private bool IsRunning => _elapsedTime < .1f;

    protected override void Update() {
        base.Update();
        _character.CharacterAnimation.SetBool("IsRunning", IsRunning);
        _character.CharacterAnimation.SetBool("IsGrounded", IsGrounded);
    }

    public void Initialize(Character character) {
        _character = character;
    }
}