using System.Collections.Generic;
using UnityEngine;

public static class LineRendererExtensions
{
	public static Vector3[] GetPoints(this LineRenderer _lineRenderer)
	{
		List<Vector3> points = new List<Vector3>();
		for (int i = 0; i < _lineRenderer.positionCount; i++)
			points.Add(_lineRenderer.GetPosition(i));

		return points.ToArray();
	}
	
	public static List<Vector3> GetListPoints(this LineRenderer _lineRenderer)
	{
		List<Vector3> points = new List<Vector3>();
		for (int i = 0; i < _lineRenderer.positionCount; i++)
			points.Add(_lineRenderer.GetPosition(i));

		return points;
	}
}
