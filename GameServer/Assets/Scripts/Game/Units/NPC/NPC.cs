using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Unit
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private NPCArea _area;
    private TransformData _lastTransform;
    private bool _isResting;

    private NPCAnimationData Animation => new NPCAnimationData(_animator.GetBool("IsRunning"));

    private TransformData TransformData => new TransformData(transform);

    private void SetRandomDestination() {
        _navMeshAgent.SetDestination(_area.RandomPosition);
        _animator.SetBool("IsRunning", true);
        _isResting = false;
    }

    private IEnumerator Rest() {
        _isResting = true;
        yield return new WaitForSeconds(Random.Range(5, 20));
        SetRandomDestination();
    }

    private void Update() {
        if (_area.Animal.Mobile && !_isResting && Vector3.Distance(transform.position, _navMeshAgent.destination) <= 1) {
            _navMeshAgent.ResetPath();
            _animator.SetBool("IsRunning", false);
            StartCoroutine(Rest());
        }
    }

    private void Start() {
        if (_area.Animal.Mobile)
            SetRandomDestination();
    }

    private void Awake() {
        _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        _navMeshAgent.angularSpeed = 1000f;
        _navMeshAgent.radius = 1f;
        _animator = GetComponent<Animator>();
        _lastTransform = TransformData.Zero;
    }

    public void Initialize(NPCArea area) {
        _area = area;
        _navMeshAgent.speed = area.Animal.MovementSpeed;
    }

    public bool UpdateTransformIfChanged() {
        if (_lastTransform.Equals(TransformData))
            return false;
        else {
            _lastTransform = TransformData;
            return true;
        }
    }   

    public NPCData Data => new NPCData(Id, TransformData, Animation, _area.Animal.name);
}
