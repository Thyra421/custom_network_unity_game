using UnityEngine;

public class Node : NetworkObject
{
    private bool _isOnRange;
    private Outline _outline;

    private void OnTriggerEnter(Collider other) {
        ColorUtility.TryParseHtmlString("#DEA805", out Color color);
        _outline.OutlineColor = color;
        _isOnRange = true;
    }

    private void OnTriggerExit(Collider other) {
        _outline.OutlineColor = Color.white;
        _isOnRange = false;
    }

    private void Update() {
        LayerMask allLayersExceptPlayer = ~(1 << LayerMask.NameToLayer("Character"));
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, allLayersExceptPlayer) && hit.transform == transform) {
            _outline.enabled = true;

            if (Input.GetMouseButtonUp(1) && _isOnRange)
                TCPClient.Send(new MessagePickUp(_id));
        }
        else
            _outline.enabled = false;
    }

    private void Awake() {
        _outline = gameObject.AddComponent<Outline>();
        _outline.enabled = false;
    }

    public delegate void OnChangedHandler(int remainingLoots);
}
