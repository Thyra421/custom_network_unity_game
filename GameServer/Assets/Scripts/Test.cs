using UnityEngine;

public class Test : MonoBehaviour
{
    public void ADD_ITEM(Item i) {
        API.Clients.Clients.ForEach((Client c) => c.Player?.Inventory.Add(i, 1, true));
    }
}
