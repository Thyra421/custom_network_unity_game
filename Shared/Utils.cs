using Newtonsoft.Json.Linq;
using System;

public static class Utils
{
    public static string GenerateUUID() => Guid.NewGuid().ToString();

    public static T ParseJsonString<T>(string s) {
        return JObject.Parse(s).ToObject<T>();
    }

    public static string ObjectToString(object o) {
        return JObject.FromObject(o).ToString();
    }
}
