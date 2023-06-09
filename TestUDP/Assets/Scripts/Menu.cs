using Newtonsoft.Json.Linq;
using System;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button connect;
    [SerializeField] Button play;
    [SerializeField] Slider slider;

    private void SetLoading()
    {
        slider.gameObject.SetActive(true);
        connect.gameObject.SetActive(false);
        play.gameObject.SetActive(false);
    }

    private void SetPlay()
    {
        slider.gameObject.SetActive(false);
        connect.gameObject.SetActive(false);
        play.gameObject.SetActive(true);
    }

    private void SetConnect()
    {
        slider.gameObject.SetActive(false);
        connect.gameObject.SetActive(true);
        play.gameObject.SetActive(false);
    }

    private void ConnectHTTP()
    {
        slider.value = 0;
        StartCoroutine(NetworkManager.current.http.Get("connect", HTTPConnectCallback));
    }

    private void ConnectTCP()
    {
        slider.value = 1;
        NetworkManager.current.tcp.Connect((object sender, EventArgs e) => ConnectUDP());
    }

    private void ConnectUDP()
    {
        slider.value = 2;
        NetworkManager.current.udp.Connect(SetPlay);
    }

    void HTTPConnectCallback(HTTPResponse response)
    {
        if (response.status == HttpStatusCode.OK)
        {
            JObject jsonMessage = JObject.Parse(response.body);
            string id = jsonMessage["id"].ToString();
            NetworkManager.current.SetId(id);
            ConnectTCP();
        }
        else
            SetConnect();
    }

    void Play() => StartCoroutine(Utils.LoadSceneAsync("Game"));

    void Start()
    {
        SetLoading(); 
        ConnectHTTP();
    }

    private void Awake()
    {
        slider.minValue = 0;
        slider.maxValue = 3;
        connect.onClick.AddListener(ConnectHTTP);
        play.onClick.AddListener(Play);
    }
}