using UnityEngine;
	
namespace FoxEngine
{
	public static class RectExtension
	{
		public static Rect MinWidth(this Rect _rect, float _minWidth)
		{
			_rect.width = Mathf.Max(_rect.width, _minWidth);
			return _rect;
		}
		
		public static Rect MaxWidth(this Rect _rect, float _maxWidth)
		{
			_rect.width = Mathf.Min(_rect.width, _maxWidth);
			return _rect;
		}
		
		public static Rect MinHeight(this Rect _rect, float _minHeight)
		{
			_rect.height = Mathf.Max(_rect.height, _minHeight);
			return _rect;
		}
		
		public static Rect MaxHeight(this Rect _rect, float _maxHeight)
		{
			_rect.height = Mathf.Min(_rect.height, _maxHeight);
			return _rect;
		}
	}
}
