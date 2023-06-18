using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public delegate void OnSuccessHandler<T>(T message);

public static class HTTPClient
{
    private static IEnumerator Get<T>(string route, OnSuccessHandler<T> onSuccess, Action onError = null) {
        string url = $"{Config.ServerAddress}:{Config.ServerPortHTTP}/{route}";
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        uwr.SendWebRequest();
        while (!uwr.isDone)
            yield return null;
        if ((HttpStatusCode)uwr.responseCode == HttpStatusCode.OK)
            onSuccess(Utils.Deserialize<T>(uwr.downloadHandler.text));
        else
            onError?.Invoke();
    }

    private static IEnumerator Post<T1, T2>(string route, T1 message, OnSuccessHandler<T2> onSuccess, Action onError = null) {
        string url = $"http://{Config.ServerAddress}:{Config.ServerPortHTTP}/{route}";
        UnityWebRequest uwr = UnityWebRequest.Post(url, Utils.JsonEncode(message), "application/json");
        uwr.SendWebRequest();
        while (!uwr.isDone)
            yield return null;
        string data = uwr.downloadHandler.text;
        Debug.Log("[HTTPClient] received " + data);
        if ((HttpStatusCode)uwr.responseCode == HttpStatusCode.OK)
            onSuccess(Utils.JsonDecode<T2>(data));
        else
            onError?.Invoke();
    }

    public static IEnumerator Login(MessageLogin messageLogin, OnSuccessHandler<MessageSecret> onSuccess, Action onError = null) {
        return Post("login", messageLogin, onSuccess, onError);
    }
}