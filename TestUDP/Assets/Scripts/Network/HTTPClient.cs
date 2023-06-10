using System;
using System.Collections;
using System.Net;
using UnityEngine.Networking;

public delegate void OnSuccessHandler<T>(T message) where T : ServerMessage;

public class HTTPClient
{
    private readonly string SERVER_URL;

    public HTTPClient(string serverUrl) {
        SERVER_URL = serverUrl;
    }

    private IEnumerator Get<T>(string route, OnSuccessHandler<T> onSuccess, Action onError = null) where T : ServerMessage {
        string url = $"{SERVER_URL}/{route}";
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        uwr.SendWebRequest();
        while (!uwr.isDone)
            yield return null;
        if ((HttpStatusCode)uwr.responseCode == HttpStatusCode.OK)
            onSuccess(Utils.ParseJsonString<T>(uwr.downloadHandler.text));
        else
            onError?.Invoke();
    }

    public IEnumerator GetPlay(OnSuccessHandler<ServerMessagePositions> onSuccess, Action onError = null) {
        return Get($"play?id={NetworkManager.current.GetId}", onSuccess, onError);
    }

    public IEnumerator GetConnect(OnSuccessHandler<ServerMessageMe> onSuccess, Action onError = null) {
        return Get($"connect", onSuccess, onError);
    }
}