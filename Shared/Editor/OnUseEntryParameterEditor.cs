using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(OnUseEntryParameter))]
public class OnUseEntryParameterEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        SerializedProperty parameterValueProperty = property.FindPropertyRelative("_parameterValue");
        SerializedProperty typeNameProperty = property.FindPropertyRelative("_typeName");
        SerializedProperty parameterNameProperty = property.FindPropertyRelative("_parameterName");
        string valueString = parameterValueProperty.stringValue;
        Type type = Type.GetType(typeNameProperty.stringValue);

        EditorGUI.PrefixLabel(position, new GUIContent(parameterNameProperty.stringValue));

        if (type == typeof(int)) {
            int newValue = EditorGUILayout.IntField(int.TryParse(valueString, out int result) ? result : 0);
            parameterValueProperty.stringValue = newValue.ToString();
        } else if (type == typeof(string)) {
            string newValue = EditorGUILayout.TextField(valueString);
            parameterValueProperty.stringValue = newValue;
        } else if (type == typeof(bool)) {
            bool newValue = EditorGUILayout.Toggle(bool.TryParse(valueString, out bool result) && result);
            parameterValueProperty.stringValue = newValue.ToString();
        } else {
            EditorGUILayout.HelpBox($"Unsupported type {type.FullName}", MessageType.Error);
        }
        parameterValueProperty.serializedObject.ApplyModifiedProperties();
    }
}