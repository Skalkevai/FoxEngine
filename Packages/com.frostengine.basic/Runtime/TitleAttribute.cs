using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleAttribute : PropertyAttribute
{
    public string Title { get; private set; }
    public float SpaceBefore { get; private set; }

    public TitleAttribute(string title, float spaceBefore = 5f)
    {
        Title = title;
        SpaceBefore = spaceBefore;
    }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(TitleAttribute))]
public class HeaderDrawer : DecoratorDrawer
{
    private static GUIStyle titleStyle;

    public override float GetHeight()
    {
        TitleAttribute titleAttribute = (TitleAttribute)attribute;
        float spaceBefore = titleAttribute.SpaceBefore;
        float spaceAfter = EditorGUIUtility.standardVerticalSpacing;

        return spaceBefore + EditorGUIUtility.singleLineHeight + spaceAfter + 3f; // Height of the header line including spacing
    }

    public override void OnGUI(Rect position)
    {
        TitleAttribute titleAttribute = (TitleAttribute)attribute;
        string title = titleAttribute.Title;
        float spaceBefore = titleAttribute.SpaceBefore;

        if (titleStyle == null)
        {
            titleStyle = new GUIStyle(EditorStyles.boldLabel);
            titleStyle.padding = new RectOffset(5, 0, 2, 2);
        }

        float offsetXLabel = position.x - 4f;

        Rect titleRect = new Rect(offsetXLabel, position.y + spaceBefore, position.width, EditorGUIUtility.singleLineHeight);
        Rect separatorRect = new Rect(position.x, position.y + spaceBefore + EditorGUIUtility.singleLineHeight, position.width, 1f);

        EditorGUI.LabelField(titleRect, title, titleStyle);
        EditorGUI.DrawRect(separatorRect, new Color(0.5f,0.5f,0.5f,1));
    }
}
#endif
