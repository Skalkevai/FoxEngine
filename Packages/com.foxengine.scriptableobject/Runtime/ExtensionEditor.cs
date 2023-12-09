using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class ExtensionEditor
{
    public static void DrawInspectorExcept(this SerializedObject serializedObject, string fieldToSkip)
    {
        serializedObject.DrawInspectorExcept(new string[1] { fieldToSkip });
    }

    public static void DrawInspectorExcept(this SerializedObject serializedObject, string[] fieldsToSkip)
    {
        serializedObject.Update();
        SerializedProperty prop = serializedObject.GetIterator();
        if (prop.NextVisible(true))
        {
            do
            {
                if (fieldsToSkip.Any(prop.name.Contains))
                    continue;

                EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name), true);
            }
            while (prop.NextVisible(false));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
