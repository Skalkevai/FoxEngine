
using UnityEngine;

public static class ColorExtensions
{
    public static string GetHexCode(this Color _color)
    {
        return "#" + ColorUtility.ToHtmlStringRGBA(_color);    
    }

    public static Color ChangeAlpha(this Color _color,float _newAlpha)
    {
        return new Color(_color.r,_color.g,_color.b,_newAlpha);
    }

    public static string ColorText(this Color _color, string _stringToColor)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(_color)}>{_stringToColor}</color>";
    }
}
