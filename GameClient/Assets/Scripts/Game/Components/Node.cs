using UnityEngine;

public class Node : MonoBehaviour
{
    private string _id;
    private bool _isOnRange;
    private Outline _outline;
    private int _remainingLoots;
    private event OnChangedHandler _onChanged;

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

    public void RemoveLoot() {
        _remainingLoots--;
        _onChanged?.Invoke(_remainingLoots);
    }

    public void Initialize(string id, int remainingDrops) {
        _id = id;
        _remainingLoots = remainingDrops;
    }

    public delegate void OnChangedHandler(int remainingLoots);

    public string Id => _id;

    public int RemainingLoots => _remainingLoots;

    public event OnChangedHandler OnChangedEvent {
        add => _onChanged += value;
        remove => _onChanged -= value;
    }
}
