using UnityEngine;

public class NPCAnimation : CharacterAnimation
{
    public NPCAnimation(Animator animator) {
        _animator = animator;
    }

    public void SetAnimation(NPCAnimationData NPCAnimationData) {
        SetBool("IsRunning", NPCAnimationData.isRunning);
    }
}