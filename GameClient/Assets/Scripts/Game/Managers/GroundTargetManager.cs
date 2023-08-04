using UnityEngine;

public class GroundTargetManager : Singleton<GroundTargetManager>
{
    [SerializeField]
    private LayerMask _whatIsGround;
    private GameObject _currentTarget;

    public bool HasTarget => _currentTarget != null;
    public Vector3Data TargetPosition => new Vector3Data(_currentTarget.transform.position);

    private void Update() {
        if (!HasTarget)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, _whatIsGround))
            _currentTarget.transform.position = hit.point;
    }

    public void DestroyGroundTarget() {
        Destroy(_currentTarget);
    }

    public void CreateGroundTarget(GameObject prefab) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, _whatIsGround))
            _currentTarget = Instantiate(prefab, hit.point, Quaternion.identity);
    }    
}
