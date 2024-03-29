using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Frost;

#if UNITY_EDITOR
using UnityEditor;
[CustomPropertyDrawer(typeof(SerializedSO), true)]
public class ScriptableObjectDrawer : PropertyDrawer
{
    // Cached scriptable object editor
    private Editor editor = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw label
        EditorGUI.PropertyField(position, property, label, true);

        // Draw foldout arrow
        if (property.objectReferenceValue != null)
        {
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
        }

        // Draw foldout properties
        if (property.isExpanded)
        {
            // Make child fields be indented
            EditorGUI.indentLevel++;

            // Draw object properties
            if (!editor)
                Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);

            GUILayout.BeginVertical(GUI.skin.box);
            editor?.OnInspectorGUI();
            GUILayout.EndVertical();

            // Set indent back to what it was
            EditorGUI.indentLevel--;
        }
    }

    [CustomEditor(typeof(SerializedSO))]
    public class SerializedSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawInspectorExcept(serializedObject, new string[1] { "m_Script" });
        }

        public static void DrawInspectorExcept(SerializedObject serializedObject, string[] fieldsToSkip)
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
}
#endif