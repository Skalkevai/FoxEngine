using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using static ScriptableObjectDrawer;

[CustomEditor(typeof(VoidEventChannel))]
public class VoidEventChannelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SerializedSOEditor.DrawInspectorExcept(serializedObject, new string[] { "m_Script" });
        VoidEventChannel eventChannel = (VoidEventChannel) target;

        GUILayout.Space(10);
        GUILayout.Label("Test Channel");
        if (GUILayout.Button("Execute Event"))
            eventChannel.ExecuteEvent();
    }
}

[CustomEditor(typeof(StringEventChannel))]
public class StringEventChannelEditor : Editor
{
    public string text;

    public override void OnInspectorGUI()
    {
        SerializedSOEditor.DrawInspectorExcept(serializedObject, new string[] { "m_Script" });
        StringEventChannel eventChannel = (StringEventChannel)target;

        GUILayout.Space(10);
        GUILayout.Label("Test Channel");
        text = GUILayout.TextField(text);
        if (GUILayout.Button("Execute Event"))
            eventChannel.ExecuteEvent(text);
    }
}

[CustomEditor(typeof(FloatEventChannel))]
public class FloatEventChannelEditor : Editor
{
    public float number;

    public override void OnInspectorGUI()
    {
        SerializedSOEditor.DrawInspectorExcept(serializedObject, new string[] { "m_Script" });
        FloatEventChannel eventChannel = (FloatEventChannel)target;

        GUILayout.Space(10);
        GUILayout.Label("Test Channel");
        number = EditorGUILayout.FloatField(number);
        if (GUILayout.Button("Execute Event"))
            eventChannel.ExecuteEvent(number);
    }
}

[CustomEditor(typeof(IntEventChannel))]
public class IntEventChannelEditor : Editor
{
    public int number;

    public override void OnInspectorGUI()
    {
        SerializedSOEditor.DrawInspectorExcept(serializedObject, new string[] { "m_Script" });
        IntEventChannel eventChannel = (IntEventChannel)target;

        GUILayout.Space(10);
        GUILayout.Label("Test Channel");
        number = EditorGUILayout.IntField(number);
        if (GUILayout.Button("Execute Event"))
            eventChannel.ExecuteEvent(number);
    }
}

[CustomEditor(typeof(ScriptableObjectEventChannel))]
public class ScriptableObjectEventChannelEditor : Editor
{
    public ScriptableObject so;

    public override void OnInspectorGUI()
    {
        SerializedSOEditor.DrawInspectorExcept(serializedObject, new string[] { "m_Script" });
        ScriptableObjectEventChannel eventChannel = (ScriptableObjectEventChannel)target;

        GUILayout.Space(10);
        GUILayout.Label("Test Channel");
        so = EditorGUILayout.ObjectField(so,typeof(ScriptableObject),false) as ScriptableObject;
        if (GUILayout.Button("Execute Event"))
            eventChannel.ExecuteEvent(so);
    }
}

[CustomEditor(typeof(ObjectEventChannel))]
public class ObjectEventChannelEditor : Editor
{
    public Object obj;

    public override void OnInspectorGUI()
    {
        SerializedSOEditor.DrawInspectorExcept(serializedObject, new string[] { "m_Script" });
        ObjectEventChannel eventChannel = (ObjectEventChannel)target;

        GUILayout.Space(10);
        GUILayout.Label("Test Channel");
        obj = EditorGUILayout.ObjectField(obj, typeof(Object),true);
        if (GUILayout.Button("Execute Event"))
            eventChannel.ExecuteEvent(obj);
    }
}
#endif