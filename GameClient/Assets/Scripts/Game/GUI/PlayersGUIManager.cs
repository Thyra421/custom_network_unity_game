using UnityEngine;

public class PlayersGUIManager : MonoBehaviour
{
    public static PlayersGUIManager Current { get; private set; }

    private void Awake() {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);
    }
}