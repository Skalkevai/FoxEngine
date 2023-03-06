using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FoxEngine;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class SpriteExtensions
{
    
#if UNITY_EDITOR
    public static int GetSpriteIndex(this Sprite _sprite)
    {
        Sprite[] sprites = _sprite.GetAllSprites();
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i] == _sprite)
                return i;
        }

        return -1;
    }

    static Texture2D GenerateTextureFromSprite(this Sprite _sprite)
    {
        var rect = _sprite.rect;
        var tex = new Texture2D((int)rect.width, (int)rect.height);
        var data = _sprite.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
        tex.SetPixels(data);
        tex.Apply(true);
        return tex;
    }
    
    public static Sprite[] GetAllSprites(this Sprite _sprite)
    {
        Texture2D texture = _sprite.texture;
        string spritePath = AssetDatabase.GetAssetPath(texture);
        return AssetDatabase.LoadAllAssetsAtPath(spritePath).OfType<Sprite>().CustomSort().ToArray();
    }
    
    public static Sprite[] GetAllSprites(this Texture2D _texture)
    {
        string spritePath = AssetDatabase.GetAssetPath(_texture);
        Object[] allSprites = AssetDatabase.LoadAllAssetsAtPath(spritePath);
        if (allSprites.Length == 0)
            return Array.Empty<Sprite>();

        Sprite[] sprites = allSprites.OfType<Sprite>().ToArray();
        if(sprites.Length == 0)
            return Array.Empty<Sprite>();
        
        return allSprites.OfType<Sprite>().CustomSort().ToArray();
    }
    
    public static IEnumerable<Sprite> CustomSort(this IEnumerable<Sprite> list)
    {
        int maxLen = list.Select(s => s.name.Length).Max();

        return list.Select(s => new
        {
            OrgStr = s,
            SortStr = Regex.Replace(s.name, @"(\d+)|(\D+)", m => m.Value.PadLeft(maxLen, char.IsDigit(m.Value[0]) ? ' ' : '\xffff'))
        })
        .OrderBy(x => x.SortStr)
        .Select(x => x.OrgStr);
    }
    
    public static Texture2D ResizeTexture(this Texture2D source, int newWidth, int newHeight)
    {
        source.filterMode = FilterMode.Point;
        RenderTexture rt = RenderTexture.GetTemporary(newWidth, newHeight);
        rt.filterMode = FilterMode.Point;
        RenderTexture.active = rt;
        Graphics.Blit(source, rt);
        Texture2D nTex = new Texture2D(newWidth, newHeight);
        nTex.ReadPixels(new Rect(0, 0, newWidth, newHeight), 0,0);
        nTex.Apply();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);
        return nTex;
    }
    
    public static Texture2D TextureFromSprite(this Sprite _sprite)
    {
        if(_sprite.rect.width != _sprite.texture.width){
            Texture2D newText = new Texture2D((int)_sprite.rect.width,(int)_sprite.rect.height);
            Color[] newColors = _sprite.texture.GetPixels((int)_sprite.textureRect.x, 
                (int)_sprite.textureRect.y, 
                (int)_sprite.textureRect.width, 
                (int)_sprite.textureRect.height );
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        } else
            return _sprite.texture;
    }
#endif
}