using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public static class HTTPClient
{
    public delegate void OnSuccessHandler<T>(T message);
    public delegate void OnFailureHandler();

    private static IEnumerator Get<T>(string route, OnSuccessHandler<T> onSuccess, OnFailureHandler onError = null) {
        //string url = $"{Config.Current.ServerAddress}:{Config.Current.ServerPortHTTP}/{route}";
        //UnityWebRequest uwr = UnityWebRequest.Get(url);
        //uwr.SendWebRequest();
        //while (!uwr.isDone)
        //    yield return null;
        //if ((HttpStatusCode)uwr.responseCode == HttpStatusCode.OK)
        //    onSuccess?.Invoke(Utils.Deserialize(uwr.downloadHandler.text, typeof<T>));
        //else
        //    onError?.Invoke();
        return null;
    }

    private static IEnumerator Post<T1, T2>(string route, T1 message, OnSuccessHandler<T2> onSuccess, OnFailureHandler onError = null) {
        string url = $"http://{Config.Current.ServerAddress}:{Config.Current.ServerPortHTTP}/{route}";
        UnityWebRequest uwr = UnityWebRequest.Post(url, Utils.JsonEncode(message), "application/json");
        uwr.SendWebRequest();
        while (!uwr.isDone)
            yield return null;
        string data = uwr.downloadHandler.text;
        Debug.Log("[HTTPClient] received " + data);
        if ((HttpStatusCode)uwr.responseCode == HttpStatusCode.OK)
            onSuccess?.Invoke(Utils.JsonDecode<T2>(data));
        else
            onError?.Invoke();
    }

    public static IEnumerator Login(MessageLogin messageLogin, OnSuccessHandler<MessageSecret> onSuccess, OnFailureHandler onError = null) {
        return Post("login", messageLogin, onSuccess, onError);
    }
}