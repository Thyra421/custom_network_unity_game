using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EffectParameter))]
public class EffectParameterEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        // get properties
        SerializedProperty parameterValueProperty = property.FindPropertyRelative("_parameterValue");
        SerializedProperty typeNameProperty = property.FindPropertyRelative("_typeName");
        SerializedProperty parameterNameProperty = property.FindPropertyRelative("_parameterName");
        // get value (non-parsed) and type
        Type type = Type.GetType(typeNameProperty.stringValue);
        object valueObject = ((EffectParameter)property.boxedValue).ToObject;
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
        } else if (type == typeof(GameObject)) {
            EditorGUILayout.HelpBox($"Prefab must be located in {SharedConfig.PREFABS_PATH}", MessageType.Info);
            GameObject newValue = (GameObject)EditorGUILayout.ObjectField((GameObject)valueObject, typeof(GameObject), false);
            parameterValueProperty.stringValue = newValue?.name;
        } else if (type == typeof(Alteration)) {
            EditorGUILayout.HelpBox($"Alteration must be located in {SharedConfig.ALTERATIONS_PATH}", MessageType.Info);
            Alteration newValue = (Alteration)EditorGUILayout.ObjectField((Alteration)valueObject, typeof(Alteration), false);
            parameterValueProperty.stringValue = newValue?.name;
        } else {
            EditorGUILayout.HelpBox($"Unsupported type {type.FullName}", MessageType.Error);
        }
    }
}