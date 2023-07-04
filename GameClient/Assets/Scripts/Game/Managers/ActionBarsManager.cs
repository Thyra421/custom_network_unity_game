using UnityEngine;

public class ActionBarManager : MonoBehaviour
{
    private static ActionBarManager _current;
    private readonly ActionBarSlot[] _slots = new ActionBarSlot[Config.ACTION_BAR_SLOTS];

    private void Awake() {
        if (_current == null)
            _current = this;
        else
            Destroy(gameObject);
        for (int i = 0; i < _slots.Length; i++)
            _slots[i] = new ActionBarSlot();
    }

    public static ActionBarManager Current => _current;

    public ActionBarSlot[] Slots => _slots;
}