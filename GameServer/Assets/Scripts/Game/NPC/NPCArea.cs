using System.Collections.Generic;
using UnityEngine;

public class NPCArea : MonoBehaviour
{
    [SerializeField]
    private GameObject _NPCTemplate;
    [SerializeField]
    private float _radius = 10;
    private readonly List<NPC> _NPCs = new List<NPC>();

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void Start() {
        for (int i = 0; i < 5; i++) {
            NPC newNPC = Instantiate(_NPCTemplate, RandomPosition, Quaternion.identity, transform).GetComponent<NPC>();
            newNPC.Initialize(this);
        }
    }

    public Vector3 RandomPosition {
        get {
            Vector2 randomPoint = Random.insideUnitCircle * _radius;
            Vector3 randomPosition = new Vector3(randomPoint.x, 0f, randomPoint.y);
            return transform.position + randomPosition;
        }
    }

    public float Radius => _radius;
}