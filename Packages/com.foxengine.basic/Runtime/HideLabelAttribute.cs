using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HideLabelAttribute : PropertyAttribute
{
    public HideLabelAttribute() { }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(HideLabelAttribute))]
public class HideLabelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, new GUIContent(" "));
    }
}
#endif
