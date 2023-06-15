using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private Transform _target;

    void Update()
    {
        transform.position = _offset + _target.position;
        
    }
}
