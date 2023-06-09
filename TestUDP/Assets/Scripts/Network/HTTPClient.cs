using System.Collections;
using System.Net;
using UnityEngine.Networking;

public class HTTPResponse
{
    public string body;
    public HttpStatusCode status;

    public HTTPResponse(string body, HttpStatusCode status)
    {
        this.body = body;
        this.status = status;
    }
}

public delegate void HttpResponseCallback(HTTPResponse response);

public class HTTPClient
{
    private readonly string SERVER_URL;

    public HTTPClient(string serverUrl)
    {
        SERVER_URL = serverUrl;
    }

    /// <summary>
    /// Must be called in a Coroutine.
    /// Example StartCoroutine(NetworkManager.current.http.Get("connect", ConnectCallback));
    /// </summary>
    public IEnumerator Get(string route, HttpResponseCallback callback)
    {
        string url = $"{SERVER_URL}/{route}";
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        uwr.SendWebRequest();
        while (!uwr.isDone) yield return null;
        callback(new HTTPResponse(uwr.downloadHandler.text, (HttpStatusCode)uwr.responseCode));
    }
}