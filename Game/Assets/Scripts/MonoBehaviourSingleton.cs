using UnityEngine;

public class MonoBehaviourSingleton : MonoBehaviour
{
    private static MonoBehaviourSingleton _current;
    [SerializeField]
    private bool _dontDestroOnLoad;

    private void Awake() {
        if (_current == null) {
            _current = this;
            if (_dontDestroOnLoad)
                DontDestroyOnLoad(gameObject);
        } else
            Destroy(gameObject);
    }

    public static MonoBehaviourSingleton Current => _current;
}
