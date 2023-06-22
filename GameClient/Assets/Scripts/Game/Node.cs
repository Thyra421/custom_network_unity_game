using UnityEngine;
using UnityEngine.AI;

public class Node : MonoBehaviour
{
    private string _id;
    private bool _isOnRange;
    private Outline _outline;
    private int _remainingLoots;

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
            TCPClient.Send(new MessagePickUp(_id));
    }

    private void Start() {
        if (_outline == null) {
            _outline = gameObject.AddComponent<Outline>();
            _outline.enabled = false;
        }
    }

    public void RemoveLoot() {
        _remainingLoots--;
        onChanged(_remainingLoots);
    }

    public delegate void OnChangedHandler(int remainingLoots);

    public string Id {
        get => _id;
        set => _id = value;
    }

    public int RemainingLoots {
        get => _remainingLoots;
        set => _remainingLoots = value;
    }

    public OnChangedHandler onChanged;
}
