using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public delegate void OnSuccessHandler<T>(T message) where T : ServerMessage;

public static class HTTPClient
{
    private static IEnumerator Get<T>(string route, OnSuccessHandler<T> onSuccess, Action onError = null) where T : ServerMessage {
        string url = $"{Config.ServerAddress}:{Config.ServerPortHTTP}/{route}";
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        uwr.SendWebRequest();
        while (!uwr.isDone)
            yield return null;
        if ((HttpStatusCode)uwr.responseCode == HttpStatusCode.OK)
            onSuccess(Utils.ParseJsonString<T>(uwr.downloadHandler.text));
        else
            onError?.Invoke();
    }

    private static IEnumerator Post<T>(string route, ClientMessage message, OnSuccessHandler<T> onSuccess, Action onError = null) where T : ServerMessage {
        string url = $"http://{Config.ServerAddress}:{Config.ServerPortHTTP}/{route}";
        UnityWebRequest uwr = UnityWebRequest.Post(url, Utils.ObjectToString(message), "application/json");
        uwr.SendWebRequest();
        while (!uwr.isDone)
            yield return null;
        string data = uwr.downloadHandler.text;
        Debug.Log("[HTTPServer] received " + data);
        if ((HttpStatusCode)uwr.responseCode == HttpStatusCode.OK)
            onSuccess(Utils.ParseJsonString<T>(data));
        else
            onError?.Invoke();
    }

    public static IEnumerator Login(ClientMessageLogin messageLogin, OnSuccessHandler<ServerMessageSecret> onSuccess, Action onError = null) {
        return Post("login", messageLogin, onSuccess, onError);
    }

    //public static IEnumerator Play(ClientMessagePlay messagePlay, OnSuccessHandler<ServerMessageGameState> onSuccess, Action onError = null) {
    //    return Post("play", messagePlay, onSuccess, onError);
    //}
}