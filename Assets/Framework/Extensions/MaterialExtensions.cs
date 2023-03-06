using UnityEngine;

namespace FoxEngine
{
	public static class MaterialExtensions
	{
		public static void ChangeColor (this Renderer _renderer,Color _color)
		{
			MaterialPropertyBlock block = new MaterialPropertyBlock();

            block.SetColor("_Color", _color);

            _renderer.SetPropertyBlock(block);
		}
		
		public static void ChangeTexture (this Renderer _renderer,Texture _tex)
		{
			MaterialPropertyBlock block = new MaterialPropertyBlock();

            block.SetTexture("_MainTex", _tex);

            _renderer.SetPropertyBlock(block);
		}
		
		public static void ChangeColor (this Renderer _renderer,string _colorName,Color _color)
		{
			MaterialPropertyBlock block = new MaterialPropertyBlock();
			block.SetColor(_colorName,_color);
			_renderer.SetPropertyBlock(block);
		}
		
		public static void ChangeTexture (this Renderer _renderer,string _textureName,Texture _tex)
		{
			MaterialPropertyBlock block = new MaterialPropertyBlock();
			block.SetTexture(_textureName,_tex);
			_renderer.SetPropertyBlock(block);
		}
		
		public static void ChangeFloat (this Renderer _renderer,string _floatName,float _value)
		{
			MaterialPropertyBlock block = new MaterialPropertyBlock();
			block.SetFloat(_floatName,_value);
			_renderer.SetPropertyBlock(block);
		}
		
		public static void ChangeInteger (this Renderer _renderer,string _intName,int _value)
		{
			MaterialPropertyBlock block = new MaterialPropertyBlock();
			block.SetInt(_intName,_value);
			_renderer.SetPropertyBlock(block);
		}
		
		public static void ChangeVector (this Renderer _renderer,string _vectorName,Vector4 _value)
		{
			MaterialPropertyBlock block = new MaterialPropertyBlock();
			block.SetVector(_vectorName,_value);
			_renderer.SetPropertyBlock(block);
		}
	}
}
