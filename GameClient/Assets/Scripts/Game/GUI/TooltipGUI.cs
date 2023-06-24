using TMPro;
using UnityEngine;

public class TooltipGUI : MonoBehaviour
{
    private static TooltipGUI _current;
    [SerializeField]
    private TMP_Text _nameText;
    [SerializeField]
    private TMP_Text _description;

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
    }

    private void Update() {
        if (gameObject.activeSelf)
            transform.position = Input.mousePosition;
    }

    public void Show(Item item) {
        _nameText.text = item.DisplayName;
        _description.text = item.Description;
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public static TooltipGUI Current => _current;
}