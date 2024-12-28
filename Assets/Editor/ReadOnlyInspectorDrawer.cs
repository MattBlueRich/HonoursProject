using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyInspectorAttribute))]
public class ReadOnlyInspectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // Hides the property.
        EditorGUI.PropertyField(position, property, label); // Draws the property to the inspector.
        GUI.enabled = true; // Anything drawn afterwards can be edited in the inspector.
    }
}
