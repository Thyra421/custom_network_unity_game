using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private float _distance;
    private Vector3 _originalPosition;
    private Vector3 _direction;

    public delegate void OnDestroyHandler();
    public event OnDestroyHandler OnDestroy;

    private void FixedUpdate() {
        transform.Translate(_speed * Time.deltaTime * _direction);
    }

    private void Update() {
        if (Vector3.Distance(_originalPosition, transform.position) >= _distance) {
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }
    }

    public void Initialize(float speed, float distance, Vector3 direction) {
        _speed = speed;
        _distance = distance;
        _direction = direction;
        _originalPosition = transform.position;
    }
}
