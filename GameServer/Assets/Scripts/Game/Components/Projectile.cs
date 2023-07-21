using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private float _distance;
    private Vector3 _originalPosition;

    public delegate void OnDestroyHandler();
    public event OnDestroyHandler OnDestroy;

    private void FixedUpdate() {
        transform.Translate(_speed * Time.deltaTime * Vector3.forward);
    }

    private void Update() {
        if (Vector3.Distance(_originalPosition, transform.position) >= _distance) { 
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }
    }

    public void Initialize(float speed, float distance) {
        _distance = distance;
        _speed = speed;
        _originalPosition = transform.position;
    }
}
