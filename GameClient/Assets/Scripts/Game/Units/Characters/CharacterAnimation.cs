using UnityEngine;

public abstract class CharacterAnimation
{
    [SerializeField]
    protected Animator _animator;

    public virtual void SetTrigger(string triggerName) {
        if (GetBool(triggerName))
            return;

        _animator.SetTrigger(triggerName);
    }

    public virtual void SetBool(string boolName, bool value) {
        _animator.SetBool(boolName, value);
    }

    public virtual void SetFloat(string floatName, float value) {
        _animator.SetFloat(floatName, value);
    }

    public bool GetBool(string boolName) {
        return _animator.GetBool(boolName);
    }

    public float GetFloat(string floatName) {
        return _animator.GetFloat(floatName);
    }

    public void Play(string animationName) {
        _animator.Play(animationName);
    }

    public void SetAnimations(AnimationData[] animations) {
        foreach (AnimationData ad in animations)
            SetBool(ad.name, ad.value);
    }
}