using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Effect), true)]
public class EffectEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        // get method names from the selected class
        string className = ((Effect)property.boxedValue).ClassName;
        Type type = Type.GetType(className);
        MethodInfo[] _methodInfos = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
        string[] methodNames = _methodInfos.Select((MethodInfo m) => m.Name).ToArray();
        // get selected method name or first if none selected
        SerializedProperty methodNameProperty = property.FindPropertyRelative("_methodName");
        int selected = string.IsNullOrEmpty(methodNameProperty.stringValue) ? 0 : Array.IndexOf(methodNames, methodNameProperty.stringValue);
        if (selected == -1)
            selected = 0;
        // show popup to select method name
        selected = EditorGUILayout.Popup(selected, methodNames);
        methodNameProperty.stringValue = methodNames[selected];
        // get parameters of the selected method
        ParameterInfo[] parameterInfos = _methodInfos[selected].GetParameters();
        SerializedProperty parametersProperty = property.FindPropertyRelative("_parameters");
        // initialize parameters properties
        parametersProperty.arraySize = parameterInfos.Length;
        for (int i = 0; i < parameterInfos.Length; i++) {
            // get parameter type and name
            string parameterTypeName = parameterInfos[i].ParameterType.AssemblyQualifiedName;
            string parameterName = parameterInfos[i].Name;
            // get parameter property and initialize it if misses parameters
            SerializedProperty parameterProperty = parametersProperty.GetArrayElementAtIndex(i);
            if (parameterProperty == null)
                parameterProperty.boxedValue = new EffectParameter(parameterTypeName, parameterInfos[i].Name);
            // get parameter property name type and value
            SerializedProperty parameterNameProperty = parameterProperty.FindPropertyRelative("_parameterName");
            SerializedProperty parameterTypeProperty = parameterProperty.FindPropertyRelative("_typeName");
            SerializedProperty parameterValueProperty = parameterProperty.FindPropertyRelative("_parameterValue");
            // name type and value if needed
            if (parameterTypeProperty.stringValue != parameterTypeName) {
                parameterTypeProperty.stringValue = parameterTypeName;
                parameterValueProperty.stringValue = "";
            }
            if (parameterNameProperty.stringValue != parameterName) {
                parameterNameProperty.stringValue = parameterInfos[i].Name;
            }
        }
        // show parameters properties
        for (int i = 0; i < parameterInfos.Length; i++)
            EditorGUILayout.PropertyField(parametersProperty.GetArrayElementAtIndex(i));
    }
}