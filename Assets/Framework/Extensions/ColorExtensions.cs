
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
}
