using System;

[Serializable]
public class RemotePlayerAnimation : CharacterAnimation
{
    public void SetAnimation(PlayerAnimationData playerAnimationData) {
        SetBool("IsRunning", playerAnimationData.isRunning);
        SetBool("IsGrounded", playerAnimationData.isGrounded);
        SetFloat("X", playerAnimationData.x);
        SetFloat("Y", playerAnimationData.y);
    }
}