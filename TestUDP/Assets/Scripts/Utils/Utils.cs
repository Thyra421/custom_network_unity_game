using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public static class Utils
{
    public static IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoad.isDone) yield return null;
    }
}