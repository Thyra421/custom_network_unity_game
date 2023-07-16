using UnityEngine;

public class Test : MonoBehaviour
{
    public void ADD_ITEM(Item i) {
        API.Clients.Clients[0].Player.Inventory.Add(i, 1, true);
    }
}
