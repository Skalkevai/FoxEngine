#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;
namespace Frost
{
	public static class HandlesExtension
	{
		public static void DrawSphere (Vector3 _pos,float _radius,Color _color)
		{
			Handles.color = _color;
			Handles.SphereHandleCap(-1,_pos,Quaternion.identity,_radius,EventType.Repaint);
		}
		
		public static void DrawSphere (Vector3 _pos,float _radius)
		{
			Handles.SphereHandleCap(-1,_pos,Quaternion.identity,_radius,EventType.Repaint);
		}
		
		public static void HandlesDrawGUI (Action _gui)
		{
			Handles.BeginGUI();
			_gui?.Invoke();
			Handles.EndGUI();
		}

	}
}
#endif
