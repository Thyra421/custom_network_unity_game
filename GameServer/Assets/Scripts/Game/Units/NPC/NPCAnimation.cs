using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    private Animator _animator;

    public NPCAnimationData Data => new NPCAnimationData(_animator.GetBool("IsRunning"));

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void SetBool(string name, bool value) {
        _animator.SetBool(name, value);
    }
}