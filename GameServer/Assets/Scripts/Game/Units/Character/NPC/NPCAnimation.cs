using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void SetBool(string name, bool value) {
        _animator.SetBool(name, value);
    }
}