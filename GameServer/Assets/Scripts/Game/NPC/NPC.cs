using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private bool _mobile;
    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    private NPCArea _area;

    private void SetRandomDestination() {
        _navMeshAgent.SetDestination(_area.RandomPosition);
    }

    private void Update() {
        if (_mobile && Vector3.Distance(transform.position, _navMeshAgent.destination) <= 1)
            SetRandomDestination();
    }

    private void Start() {
        if (_mobile)
            SetRandomDestination();
    }

    public void Initialize(NPCArea area) {
        _area = area;
    }
}
