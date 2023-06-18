using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _current;

    private void Awake() {
        if (_current == null) {
            _current = this;
            DontDestroyOnLoad(gameObject);
        } else
            Destroy(gameObject);
    }

    private static IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
            yield return null;
    }

    public static SceneLoader Current => _current;

    public void LoadMenuAsync() {
        StartCoroutine(LoadSceneAsync("Menu"));
    }

    public void LoadGameAsync() {
        StartCoroutine(LoadSceneAsync("Game"));
    }
}
