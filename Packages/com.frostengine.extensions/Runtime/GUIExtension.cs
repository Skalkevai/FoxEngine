using System;
using UnityEditor;
using UnityEngine;
namespace Frost
{
	public static class GUIExtension
	{
		public static Vector2 ScrollView (Vector2 _scrollVector,Action _drawGUI,params GUILayoutOption[] _options)
		{
			Vector2 scroll = _scrollVector;
			scroll = GUILayout.BeginScrollView(scroll,_options);
			_drawGUI?.Invoke();
			GUILayout.EndScrollView();
			return scroll;
		}
	}
}
