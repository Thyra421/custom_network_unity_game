using UnityEngine;

public abstract class CharacterAnimation
{
    [SerializeField]
    protected Animator _animator;

    public void SetTrigger(string triggerName) {
        _animator.SetTrigger(triggerName);
    }

    public void SetBool(string boolName, bool value) {
        _animator.SetBool(boolName, value);
    }

    public void SetFloat(string floatName, float value) {
        _animator.SetFloat(floatName, value);
    }

    public bool GetBool(string boolName) {
        return _animator.GetBool(boolName);
    }

    public float GetFloat(string floatName) {
        return _animator.GetFloat(floatName);
    }

    public void SetAnimations(AnimationData[] animations) {
        foreach (AnimationData ad in animations)
            SetBool(ad.name, ad.value);
    }
}