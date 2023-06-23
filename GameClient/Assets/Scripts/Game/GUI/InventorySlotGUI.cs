using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotGUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _amountText;
    [SerializeField]
    private Image _image;

    public void OnChanged(Item item, int amount) {
        if (item == null) {
            _image.gameObject.SetActive(false);
            _amountText.gameObject.SetActive(false);
        } else {
            _image.sprite = item.Icon;
            _amountText.text = amount.ToString();
            _image.gameObject.SetActive(true);
            _amountText.gameObject.SetActive(item.Property == ItemProperty.Stackable);
        }
    }
}