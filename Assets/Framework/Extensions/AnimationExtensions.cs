using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public static class AnimationExtensions
{
    public static Sprite GetFirstKeySprite(this AnimationClip _clip)
    {
        EditorCurveBinding[] curves = AnimationUtility.GetObjectReferenceCurveBindings(_clip);
        EditorCurveBinding spriteCurve = curves[0];
        ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(_clip, spriteCurve);

        return ((Sprite) keyframes[0].value);
    }

    public static Sprite GetSpriteAtIndex(this AnimationClip _clip,int _index)
    {
        EditorCurveBinding[] curves = AnimationUtility.GetObjectReferenceCurveBindings(_clip);
        EditorCurveBinding spriteCurve = curves[0];
        ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(_clip, spriteCurve);

        return ((Sprite) keyframes[_index].value);
    }
    
    public static int GetSpritesCount(this AnimationClip _clip)
    {
        EditorCurveBinding[] curves = AnimationUtility.GetObjectReferenceCurveBindings(_clip);
        EditorCurveBinding spriteCurve = curves[0];
        ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(_clip, spriteCurve);

        return keyframes.Length;
    }

    public static Texture2D[] GetAllTextures(this AnimationClip _clip)
    {
        List<Texture2D> textures = new List<Texture2D>();
        
        EditorCurveBinding[] curves = AnimationUtility.GetObjectReferenceCurveBindings(_clip);
        EditorCurveBinding spriteCurve = curves[0];
        ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(_clip, spriteCurve);

        foreach (var keyframe in keyframes)
        {
            Texture2D text = ((Sprite)keyframe.value).texture;
            if(!textures.Contains(text))
                textures.Add(text);
        }

        return textures.ToArray();
    }
}
#endif
