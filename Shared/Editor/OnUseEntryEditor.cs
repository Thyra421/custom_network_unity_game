using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(OnUseEntry))]
public class OnUseEntryEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        SerializedProperty methodNameProperty = property.FindPropertyRelative("_methodName");
        MethodInfo[] _methodInfos = typeof(OnUses).GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
        string[] methodNames = _methodInfos.Select((MethodInfo m) => m.Name).ToArray();
        int selected = string.IsNullOrEmpty(methodNameProperty.stringValue) ? 0 : Array.IndexOf(methodNames, methodNameProperty.stringValue);

        selected = EditorGUILayout.Popup(selected, methodNames);
        methodNameProperty.stringValue = methodNames[selected];

        ParameterInfo[] parameterInfos = _methodInfos[selected].GetParameters();
        SerializedProperty parametersProperty = property.FindPropertyRelative("_parameters");
        parametersProperty.arraySize = parameterInfos.Length;

        for (int i = 0; i < parameterInfos.Length; i++) {
            SerializedProperty parameterProperty = parametersProperty.GetArrayElementAtIndex(i);
            string typeName = parameterInfos[i].ParameterType.AssemblyQualifiedName;

            if (((OnUseEntryParameter)parameterProperty?.boxedValue)?.TypeName != typeName)
                parameterProperty.boxedValue = new OnUseEntryParameter(typeName, parameterInfos[i].Name);
        }
        parametersProperty.serializedObject.ApplyModifiedProperties();

        for (int i = 0; i < parameterInfos.Length; i++)
            EditorGUILayout.PropertyField(parametersProperty.GetArrayElementAtIndex(i));
    }
}