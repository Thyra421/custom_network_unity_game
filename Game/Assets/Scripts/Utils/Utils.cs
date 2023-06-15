using Newtonsoft.Json.Linq;

public static class Utils
{
    public static T ParseJsonString<T>(string s) {
        return JObject.Parse(s).ToObject<T>();
    }

    public static string ObjectToString(object o) {
        return JObject.FromObject(o).ToString();
    }
}