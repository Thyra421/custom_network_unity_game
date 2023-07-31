using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    private NPC _NPC;
    private NavMeshAgent _navMeshAgent;
    private bool _isResting;

    public NPCMovementData Data => new NPCMovementData(_NPC.Id,_NPC.TransformData, _NPC.Animation.Data, _navMeshAgent.velocity.magnitude);

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
        if (_NPC.Area.Animal.Mobile && !_isResting && Vector3.Distance(transform.position, _navMeshAgent.destination) <= 1) {
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
        _navMeshAgent.speed = _NPC.Area.Animal.MovementSpeed;
    }
}