#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
namespace Frost
{
	public static class SceneExtension
	{

		public static Vector3 GetCenterCamAtY (this SceneView _sceneView,float _y = 0)
		{
			Ray camRay = _sceneView.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			plane.Raycast(camRay, out float distance);

			return camRay.GetPoint(distance);
		}
		
		public static Vector3 MousePositionInWorld(this SceneView _sceneView,LayerMask _layerMask)
		{
			Camera cam = _sceneView.camera;

			Ray ray = cam.ScreenPointToRay(Event.current.mousePosition);
			RaycastHit hit = default;
			Physics.Raycast(ray, out hit, float.MaxValue,_layerMask);
            
			return hit.point;
		}
		
		public static Vector3 CenterScreenInWorld(this SceneView _sceneView)
		{
			Camera cam = _sceneView.camera;

			Ray ray = cam.ViewportPointToRay(new Vector3(0.5f,0.5f));
			RaycastHit hit = default;
			Physics.Raycast(ray, out hit, float.MaxValue);
            
			return hit.point;
		}
	}
}
#endif