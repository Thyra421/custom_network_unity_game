using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ActionParameter))]
public class ActionParameterEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        // get properties
        SerializedProperty parameterValueProperty = property.FindPropertyRelative("_parameterValue");
        SerializedProperty typeNameProperty = property.FindPropertyRelative("_typeName");
        SerializedProperty parameterNameProperty = property.FindPropertyRelative("_parameterName");
        // get value (non-parsed) and type
        Type type = Type.GetType(typeNameProperty.stringValue);
        object valueObject = ((ActionParameter)property.boxedValue).ToObject;
        if (valueObject == null) {
            EditorGUILayout.HelpBox($"Unsupported type {type.FullName}", MessageType.Error);
            return;
        }
        // show label
        EditorGUI.PrefixLabel(position, new GUIContent(parameterNameProperty.stringValue));
        // parse value, show it, and set the new value in the value property
        if (type == typeof(int)) {
            int newValue = EditorGUILayout.IntField((int)valueObject);
            parameterValueProperty.stringValue = newValue.ToString();
        } else if (type == typeof(string)) {
            string newValue = EditorGUILayout.TextField((string)valueObject);
            parameterValueProperty.stringValue = newValue;
        } else if (type == typeof(bool)) {
            bool newValue = EditorGUILayout.Toggle((bool)valueObject);
            parameterValueProperty.stringValue = newValue.ToString();
        } else if (type == typeof(float)) {
            float newValue = EditorGUILayout.FloatField((float)valueObject);
            parameterValueProperty.stringValue = newValue.ToString();
        } else {
            EditorGUILayout.HelpBox($"Unsupported type {type.FullName}", MessageType.Error);
        }
    }
}