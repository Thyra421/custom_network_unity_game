using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    //private Vector3 _destination;
    private float _distance;
    private Vector3 _originalPosition;

    private void FixedUpdate() {
        transform.Translate(_speed * Time.deltaTime * Vector3.forward);
    }

    private void Update() {
        if (Vector3.Distance(_originalPosition, transform.position) >= _distance)
            Destroy(gameObject);
    }

    public void Initialize(float speed, float distance) {
        _distance = distance;
        _speed = speed;
        _originalPosition = transform.position;
        //_destination = _originalPosition + transform.rotation * Vector3.forward * _distance;
    }
}
