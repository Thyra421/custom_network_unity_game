using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private string _id;
    private bool _isOnRange;
    [SerializeField]
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

    private void OnMouseUp() {
        if (_isOnRange)
            Debug.Log("Picking up " + _id);
    }

    public string Id {
        get => _id;
        set => _id = value;
    }
}
