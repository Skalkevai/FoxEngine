using System.Linq;
using System.Reflection;
using UnityEditor;

public static class UnityExtensions
{
    public static void ClearLogConsole()
    {
#if UNITY_EDITOR
        Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.SceneView));             
        System.Type logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");             
        MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");             
        clearConsoleMethod.Invoke(new object(), null);
#endif
    }
}

public static class ExtensionEditor
{
#if UNITY_EDITOR
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
#endif
}
