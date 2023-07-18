using TMPro;
using UnityEngine;

public class TooltipGUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _nameText;
    [SerializeField]
    private TMP_Text _description;

    public static TooltipGUI Current { get; private set; }

    private void Awake() {
        if (Current == null)
            Current = this;
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
}