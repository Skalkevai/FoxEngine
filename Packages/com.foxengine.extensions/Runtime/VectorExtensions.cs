using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector3Int ToVector3IntFloor(this Vector3 _vector)
    {
        return new Vector3Int(Mathf.FloorToInt(_vector.x), Mathf.FloorToInt(_vector.y), Mathf.FloorToInt(_vector.z));
    }

    public static Vector3Int ToVector3IntCeil(this Vector3 _vector)
    {
        return new Vector3Int(Mathf.CeilToInt(_vector.x), Mathf.CeilToInt(_vector.y), Mathf.CeilToInt(_vector.z));
    }

    public static Vector3Int ToVector3IntRound(this Vector3 _vector)
    {
        return new Vector3Int(Mathf.RoundToInt(_vector.x), Mathf.RoundToInt(_vector.y), Mathf.RoundToInt(_vector.z));
    }

    public static Vector3 DirectionNormalized (this Vector3 _vector,Vector3 _pointStart)
    {
        return (_vector - _pointStart).normalized;
    }
    
    public static Vector3 Direction (this Vector3 _vector,Vector3 _pointStart)
    {
        return _vector - _pointStart;
    }
    
    public static Vector2 DirectionNormalized (this Vector2 _vector,Vector2 _pointStart)
    {
        return (_vector - _pointStart).normalized;
    }
    
    public static Vector2 Direction (this Vector2 _vector,Vector2 _pointStart)
    {
        return _vector - _pointStart;
    }

    public static Vector3 FlattenY(this Vector3 _vector)
    {
        return new Vector3(_vector.x,0,_vector.z);
    }
    
    public static Vector3 FlattenZ(this Vector3 _vector)
    {
        return new Vector3(_vector.x,_vector.y,0);
    }
    
    public static Vector3 FlattenX(this Vector3 _vector)
    {
        return new Vector3(0,_vector.y,_vector.z);
    }
    
    public static Vector2Int ToVector2IntFloor(this Vector2 _vector)
    {
        return new Vector2Int(Mathf.FloorToInt(_vector.x),Mathf.FloorToInt(_vector.y));
    }
    
    public static Vector2Int ToVector2IntFloor(this Vector3 _vector)
    {
        return new Vector2Int(Mathf.FloorToInt(_vector.x),Mathf.FloorToInt(_vector.y));
    }

    public static Vector2Int ToVector2IntRound(this Vector2 _vector)
    {
        return new Vector2Int(Mathf.RoundToInt(_vector.x),Mathf.RoundToInt(_vector.y));
    }
    
    public static Vector2Int ToVector2IntRound(this Vector3 _vector)
    {
        return new Vector2Int(Mathf.RoundToInt(_vector.x),Mathf.RoundToInt(_vector.y));
    }
    
    public static Vector3 ToVector3(this Vector2Int _vector)
    {
        return new Vector3(_vector.x,_vector.y,0);
    }
    
    public static Vector2 ToVector2(this Vector2Int _vector)
    {
        return new Vector2(_vector.x,_vector.y);
    }

    public static bool InsideRect(this Vector2 _point,Vector2 _p1,Vector2 _p2)
    {
        float top = _p1.y > _p2.y?_p1.y:_p2.y;
        float bottom = _p1.y < _p2.y?_p1.y:_p2.y;
        float right = _p1.x > _p2.x?_p1.x:_p2.x;
        float left  = _p1.x < _p2.x?_p1.x:_p2.x;

        if (_point.x > left && _point.x < right)
            if (_point.y < top && _point.y > bottom)
                return true;
        
        return false;
    }

    public static Vector3? ClosestPoint(this IEnumerable<Vector3> _points,Vector3 _target)
    {
        float distance = float.MaxValue;
        Vector3? closestPoint = null;
        
        foreach (var point in _points)
        {
            float d = Vector3.Distance(point, _target);
            if (d < distance)
            {
                distance = d;
                closestPoint = point;
            }
        }
        
        return closestPoint;
    }
    
    public static Vector2? ClosestPoint(this IEnumerable<Vector2> _points,Vector2 _target)
    {
        float distance = float.MaxValue;
        Vector2? closestPoint = null;
        
        foreach (var point in _points)
        {
            float d = Vector2.Distance(point, _target);
            if (d < distance)
            {
                distance = d;
                closestPoint = point;
            }
        }

        return closestPoint;
    }
    
    public static Vector3? ClosestPoint(this IEnumerable<MonoBehaviour> _points,Vector3 _target)
    {
        float distance = float.MaxValue;
        Vector3? closestPoint = null;
        
        foreach (var point in _points)
        {
            float d = Vector3.Distance(point.transform.position, _target);
            if (d < distance)
            {
                distance = d;
                closestPoint = point.transform.position;
            }
        }
        
        return closestPoint;
    }
    
    public static Vector2? ClosestPoint(this IEnumerable<MonoBehaviour> _points,Vector2 _target)
    {
        float distance = float.MaxValue;
        Vector2? closestPoint = null;
        
        foreach (var point in _points)
        {
            float d = Vector2.Distance(point.transform.position, _target);
            if (d < distance)
            {
                distance = d;
                closestPoint = point.transform.position;
            }
        }

        return closestPoint;
    }
    
    public static Vector2 Rotate(this Vector2 _v, float _degrees)
    {
        return Quaternion.Euler(0, 0, _degrees) * _v;
    }

    public static Vector3 Rotate(this Vector3 _v, float _angle, Vector3 _axis)
    {
        return Quaternion.AngleAxis(_angle, _axis) * _v;
    }

    public static Vector3 Round (this Vector3 _v)
    {
        return new Vector3(Mathf.Round(_v.x),Mathf.Round(_v.y),Mathf.Round(_v.z));
    }

    public static Vector3 Floor (this Vector3 _v)
    {
        return new Vector3(Mathf.Floor(_v.x),Mathf.Floor(_v.y),Mathf.Floor(_v.z));
    }
    
    public static Vector3 Ceil (this Vector3 _v)
    {
        return new Vector3(Mathf.Ceil(_v.x),Mathf.Ceil(_v.y),Mathf.Ceil(_v.z));
    }
    
    public static Vector3 SnapPosition (this Vector3 _pos, float _gridSize = 1)
    {
        return new Vector3(
            Mathf.RoundToInt(_pos.x/_gridSize)*_gridSize, 
            Mathf.RoundToInt(_pos.y/_gridSize)*_gridSize, 
            Mathf.RoundToInt(_pos.z/_gridSize)*_gridSize);
    }
}
