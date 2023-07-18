using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemGUI : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TMP_Text _amountText;
    [SerializeField]
    private RectTransform _rectTransform;

    public int Amount { get; private set; }
    public Item Item { get; private set; }

    public void Initialize(Item item, int amount) {
        Item = item;
        Amount = amount;
        if (item == null) {
            _image.gameObject.SetActive(false);
            _amountText.gameObject.SetActive(false);
        } else {
            _image.sprite = item.Icon;
            _amountText.text = Amount.ToString();
            _image.gameObject.SetActive(true);
            _amountText.gameObject.SetActive(item.Property == ItemProperty.Stackable);
        }
    }

    //private void OnMouseOver() {
    //    TooltipGUI.Current.Hide();
    //}

    //private void OnMouseEnter() {
    //    TooltipGUI.Current.Show(Item);
    //}
}
