using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField]
    protected float _movementSpeed = 5f;
    [SerializeField]
    protected float _rotationSpeed = 100f;
    [SerializeField]
    protected Animator _animator;
}