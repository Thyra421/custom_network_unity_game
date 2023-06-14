namespace TestUDP;

public class HTTPServer
{
    WebApplication server = WebApplication.CreateBuilder().Build();

    public async void Start() {
        server.MapGet("", () => "OK");
        server.MapPost("/play", OnPlay);
        server.MapPost("/login", OnLogin);
        await server.RunAsync();
    }

    private static string OnLogin(object obj) {
        ClientMessageLogin messageLogin = Utils.ParseJsonString<ClientMessageLogin>(obj.ToString());
        ServerMessageSecret messageSecret = new ServerMessageSecret(messageLogin.username);
        // TODO encrypter 
        return Utils.ObjectToString(messageSecret);
    }

    private static string OnPlay(object obj) {
        ClientMessagePlay messagePlay = Utils.ParseJsonString<ClientMessagePlay>(obj.ToString());
        Client client = API.Clients.Find(messagePlay.secret);
        Player newPlayer = API.Players.Create(client);
        API.Players.BroadcastTCP(new ServerMessageJoinedGame(newPlayer.Data), newPlayer);
        ServerMessageGameState messageGameState = new ServerMessageGameState(newPlayer.Data.id, API.Players.GetObjectDatas().ToArray());
        return Utils.ObjectToString(messageGameState);
    }
}
