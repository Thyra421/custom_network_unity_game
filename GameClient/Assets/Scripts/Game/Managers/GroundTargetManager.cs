using UnityEngine;

public class GroundTargetManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _groundTargetTemplate;
    [SerializeField]
    private LayerMask _whatIsGround;
    private GameObject _currentTarget;

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool result = Physics.Raycast(ray, out RaycastHit hit, 100, _whatIsGround);

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (_currentTarget != null)
                Destroy(_currentTarget);
            else if (result)
                _currentTarget = Instantiate(_groundTargetTemplate, hit.point, Quaternion.identity);
        }

        if (_currentTarget != null)
            _currentTarget.transform.position = hit.point;
    }
}
