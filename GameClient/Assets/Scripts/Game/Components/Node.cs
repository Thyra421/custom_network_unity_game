using UnityEngine;

public class Node : MonoBehaviour
{
    private string _id;
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

    private void OnMouseEnter() {
        _outline.enabled = true;
    }

    private void OnMouseExit() {
        _outline.enabled = false;
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonUp(1) && _isOnRange)
            TCPClient.Send(new MessagePickUp(_id));
    }

    private void Awake() {
        _outline = gameObject.AddComponent<Outline>();
        _outline.enabled = false;
    }

    public void Initialize(string id) {
        _id = id;
    }

    public delegate void OnChangedHandler(int remainingLoots);

    public string Id => _id;
}
