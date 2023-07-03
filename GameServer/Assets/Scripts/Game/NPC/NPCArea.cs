using UnityEngine;
using UnityEngine.AI;

public class NPCArea : MonoBehaviour
{
    [SerializeField]
    private Animal _animal;
    [SerializeField]
    private float _radius = 10;
    [SerializeField]
    private int _amount;

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public Vector3 RandomPosition {
        get {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * _radius;
            NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, _radius, NavMesh.AllAreas);
            return hit.position;
        }
    }

    public float Radius => _radius;

    public Animal Animal => _animal;

    public int Amount => _amount;
}