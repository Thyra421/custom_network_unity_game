using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    private NPC _NPC;
    private NavMeshAgent _navMeshAgent;
    private bool _isResting;

    private bool CanMove => !_NPC.States.Find(StateType.Rooted).Value;
    private float MovementSpeed => _NPC.Statistics.Find(StatisticType.MovementSpeed).AlteredValue * _NPC.Area.Animal.MovementSpeed;

    public UnitMovementData Data => new UnitMovementData(_NPC.Id, _NPC.TransformData, MovementSpeed);

    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.angularSpeed = 1000f;
        _navMeshAgent.radius = 1f;
    }

    private void Start() {
        if (_NPC.Area.Animal.Mobile)
            SetRandomDestination();
        
    }

    private void Update() {
        // TODO optimize

        if (_navMeshAgent.speed != MovementSpeed)
            _navMeshAgent.speed = MovementSpeed;

        _navMeshAgent.isStopped = !(CanMove && _NPC.HasControl);

        if (_NPC.Target != null && _NPC.Target.transform.position != _navMeshAgent.destination)
            _navMeshAgent.SetDestination(_NPC.Target.transform.position);

        if (_NPC.Target == null && _NPC.Area.Animal.Mobile && !_isResting && Vector3.Distance(transform.position, _navMeshAgent.destination) <= 1) {
            _navMeshAgent.ResetPath();
            _NPC.Animation.SetBool("IsRunning", false);
            StartCoroutine(Rest());
        }
    }

    private void SetRandomDestination() {
        _navMeshAgent.SetDestination(_NPC.Area.RandomPosition);
        _NPC.Animation.SetBool("IsRunning", true);
        _isResting = false;
    }

    private IEnumerator Rest() {
        _isResting = true;
        yield return new WaitForSeconds(Random.Range(5, 20));
        SetRandomDestination();
    }

    public void Initialize(NPC npc) {
        _NPC = npc;
        _navMeshAgent.speed = MovementSpeed;
    }
}