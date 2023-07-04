using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemGUI : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TMP_Text _amountText;
    [SerializeField]
    private RectTransform _rectTransform;
    private int _amount;
    private Item _item;

    public void Initialize(Item item, int amount) {
        _item = item;
        _amount = amount;
        if (item == null) {
            _image.gameObject.SetActive(false);
            _amountText.gameObject.SetActive(false);
        } else {
            _image.sprite = item.Icon;
            _amountText.text = _amount.ToString();
            _image.gameObject.SetActive(true);
            _amountText.gameObject.SetActive(item.Property == ItemProperty.Stackable);
        }
    }    

    public int Amount => _amount;

    public Item Item => _item;

    //private void OnMouseOver() {
    //    TooltipGUI.Current.Hide();
    //}

    //private void OnMouseEnter() {
    //    TooltipGUI.Current.Show(_item);
    //}
}
