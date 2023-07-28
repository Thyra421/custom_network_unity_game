using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class TCPServer
{
    private TcpListener _tcpListener;

    private async void Listen() {
        if (_tcpListener == null) {
            OnError();
            return;
        }
        OnListening();
        while (true) {
            TcpClient tcpClient = await _tcpListener.AcceptTcpClientAsync();
            OnConnect(tcpClient);
        }
    }

    private static void OnConnect(TcpClient tcpClient) {
        Debug.Log("[TCPServer] client connected");

        API.Clients.Create(new TCPClient(tcpClient));
    }

    private static void OnStarted() {
        Debug.Log("[TCPServer] started");
    }

    private void OnListening() {
        Debug.Log($"[TCPServer] listening on {_tcpListener.LocalEndpoint}");
    }

    private static void OnError() {
        Debug.Log($"[TCPServer] not connected");
    }

    private static void OnConnectionFailed() {
        Debug.Log($"[TCPServer] connection failed");
    }

    public bool Start() {
        try {
            _tcpListener = new TcpListener(IPAddress.Parse(Config.Current.Address), Config.Current.TCPPort);
            _tcpListener.Start();
            OnStarted();
            Listen();
        } catch (Exception) {
            OnConnectionFailed();
            return false;
        }
        return true;
    }
}