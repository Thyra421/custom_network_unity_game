using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private readonly string _id = Utils.GenerateUUID();
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;    
    private NPCArea _area;
    private TransformData _lastTransform;

    private void SetRandomDestination() {
        _navMeshAgent.SetDestination(_area.RandomPosition);
    }

    private void Update() {
        if (_area.Animal.Mobile && Vector3.Distance(transform.position, _navMeshAgent.destination) <= 1)
            SetRandomDestination();
    }

    private void Start() {
        if (_area.Animal.Mobile)
            SetRandomDestination();
    }

    private void Awake() {
        _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        _navMeshAgent.speed = 2.5f;
        _navMeshAgent.angularSpeed = 1000f;
        _navMeshAgent.radius = 1f;
        _animator = GetComponent<Animator>();

        _lastTransform = TransformData.Zero;
    }

    public void Initialize(NPCArea area) {
        _area = area;
    }

    public bool UpdateTransformIfChanged() {
        if (_lastTransform.Equals(TransformData))
            return false;
        else {
            _lastTransform = TransformData;
            return true;
        }
    }

    public NPCAnimationData Animation => new NPCAnimationData(_animator.GetBool("IsRunning"));

    public TransformData TransformData => new TransformData(transform);

    public NPCData Data => new NPCData(_id, TransformData, Animation, _area.Animal.Prefab.name);

    public string Id => _id;
}
