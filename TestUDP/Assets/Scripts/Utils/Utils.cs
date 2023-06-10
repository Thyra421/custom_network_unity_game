using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;

public static class Utils
{
    /// <summary>
    /// Coroutine
    /// </summary>
    public static IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
            yield return null;
    }

    public static T ParseJsonString<T>(string s) {
        return JObject.Parse(s).ToObject<T>();
    }
}