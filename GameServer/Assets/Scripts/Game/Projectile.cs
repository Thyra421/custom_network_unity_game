using UnityEngine;

[RequireComponent(typeof(AttackHitbox))]
public class Projectile : MonoBehaviour
{
    private float _speed;
    private Vector3 _destination;

    private void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position, _destination, _speed * Time.deltaTime);
    }

    private void Update() {
        if (Vector3.Distance(transform.position, _destination) <= .5f)
            Destroy(gameObject);
    }

    public void Initialize(float speed, float distance) {
        _speed = speed;
        _destination = transform.forward * distance;
    }
}
