using System;

[Serializable]
public class RemotePlayerAnimation : CharacterAnimation
{
    public void SetAnimation(PlayerAnimationData animation) {
        SetFloat("X", animation.x);
        SetFloat("Y", animation.y);
    }
}