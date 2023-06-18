using Newtonsoft.Json.Linq;
using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections.Generic;
using System.Reflection;

public static class Utils
{
    public static string GenerateUUID() => Guid.NewGuid().ToString();

    public static T ParseJsonString<T>(string s) {
        return JObject.Parse(s).ToObject<T>();
    }

    public static string ObjectToString(object o) {
        return JObject.FromObject(o).ToString();
    }

    private static string SerializeValue(object value, Type type) {
        string str = "";

        if (type.IsValueType && !type.IsPrimitive) {
            MethodInfo genericMethod = typeof(Program).GetMethod(nameof(SerializeObject))!.MakeGenericMethod(type);
            str += genericMethod.Invoke(null, new object[] { value });
        } else
            str += value;
        return str;
    }

    private static string SerializeArray(object[] array, Type type) {
        string str = "";

        str += "[";
        for (int j = 0; j < array.Length; j++) {
            str += SerializeValue(array[j], type);
            if (j < array.Length - 1)
                str += ";";
        }
        str += "]";
        return str;
    }

    private static object[] DeserializeArray(List<object> list, Type type) {
        object[] result = new object[list.Count];

        for (int i = 0; i < list.Count; i++)
            result[i] = DeserializeValue(list[i], type)!;
        return result;
    }

    private static object DeserializeValue(object value, Type type) {
        if (type.IsValueType && !type.IsPrimitive) {
            MethodInfo genericMethod = typeof(Program).GetMethod(nameof(DeserializeSplitted))!.MakeGenericMethod(type);
            return genericMethod.Invoke(null, new object[] { (value as List<object>)! })!;
        } else if (type.Equals(typeof(bool)))
            return bool.Parse((value as string)!);
        else if (type.Equals(typeof(int)))
            return int.Parse((value as string)!);
        else if (type.Equals(typeof(string)))
            return value;
        else if (type.Equals(typeof(float)))
            return float.Parse((value as string)!);
        else if (type.Equals(typeof(char)))
            return char.Parse((value as string)!);
        return value;
    }

    private static T DeserializeSplitted<T>(List<object> splittedSerializedObject) {
        Type objectType = typeof(T);
        FieldInfo[] objectFields = objectType.GetFields();
        object objectInstance = Activator.CreateInstance(objectType)!;

        for (int i = 0; i < objectFields.Length; i++) {
            Type fieldType = objectFields[i].FieldType;
            object value = splittedSerializedObject[i];

            if (fieldType.IsArray) {
                List<object> subList = (splittedSerializedObject[i] as List<object>)!;
                Type arrType = fieldType.GetElementType()!;
                object[] objects = DeserializeArray(subList, arrType);
                Array array = Array.CreateInstance(arrType, objects.Length);
                for (int j = 0; j < array.Length; j++)
                    array.SetValue(objects[j], j);

                objectFields[i].SetValue(objectInstance, array);
            } else
                objectFields[i].SetValue(objectInstance, DeserializeValue(value, fieldType));
        }
        return (T)objectInstance;
    }

    /// <summary>
    /// Split the string into sub lists of strings. 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="list">List in which the sub strings will be stored.</param>
    /// <returns>The number of characters that have been treated.</returns>
    private static int Split(string s, List<object> list) {
        s = s[1..];
        string currentValue = "";
        int characters = 0;

        for (int i = 0; i < s.Length; i++) {
            if (s[i] == '}' || s[i] == ']') {
                if (currentValue.Length > 0)
                    list.Add(currentValue);
                characters++;
                break;
            } else if (s[i] == '{' || s[i] == '[') {
                List<object> l = new List<object>();
                int charactersToSkip = Split(s[i..], l);
                i += charactersToSkip;
                characters += charactersToSkip;
                list.Add(l);
            } else if (s[i] == ';') {
                if (currentValue.Length > 0 || s[i - 1] == ';')
                    list.Add(currentValue);
                currentValue = "";
            } else
                currentValue += s[i];
            characters++;
        }
        return characters;
    }

    public static T Deserialize<T>(string serializedObject) {
        List<object> splittedSerializedObject = new List<object>();
        Split(serializedObject, splittedSerializedObject);
        return DeserializeSplitted<T>(splittedSerializedObject);
    }

    public static string SerializeObject<T>(T myStruct) {
        Type structType = typeof(T);
        FieldInfo[] fields = structType.GetFields();
        string str = "{";

        for (int i = 0; i < fields.Length; i++) {
            Type type = fields[i].FieldType;
            object value = fields[i].GetValue(myStruct);

            if (type.IsArray) {
                if (value == null)
                    str += "[]";
                else {
                    Array originalArray = (Array)value;
                    object[] arr = new object[originalArray.Length];
                    Array.Copy(originalArray, arr, originalArray.Length);
                    Type arrType = type.GetElementType()!;
                    str += SerializeArray(arr, arrType);
                }
            } else if (value != null)
                str += SerializeValue(value, type);

            if (i < fields.Length - 1)
                str += ";";
        }
        str += "}";
        return str;
    }    
}
