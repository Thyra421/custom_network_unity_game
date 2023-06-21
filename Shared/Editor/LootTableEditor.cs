using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LootTable))]
public class LootTableDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        SerializedProperty entries = property.FindPropertyRelative("_entries");
        LootTable lootTable = property.boxedValue as LootTable;
        int totalWeight = lootTable.Entries.Select((LootTableEntry e) => e.DropChance).Sum();
        EditorGUILayout.LabelField(label);
        EditorGUILayout.PropertyField(entries.FindPropertyRelative("Array.size"));
        for (int i = 0; i < entries.arraySize; i++) {
            SerializedProperty entry = entries.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(entry);

            LootTableEntry lootTableEntry = entry.boxedValue as LootTableEntry;

            EditorGUILayout.LabelField((totalWeight == 0 ? 0 : lootTableEntry.DropChance * 100 / totalWeight) + "%");
        }
    }
}