using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FoxEngine
{
    public static class CollectionExtensions
    {
        public static bool IsInsideMatrix<T>(this T[,] _matrix, int _x, int _y)
        {
            return _x >= 0 && _x < _matrix.GetLength(0) && _y >= 0 && _y < _matrix.GetLength(1);
        }

        public static bool IsInsideMatrix<T>(this T[,] _matrix, Vector2Int _pos)
        {
            return _pos.x >= 0 && _pos.x < _matrix.GetLength(0) && _pos.y >= 0 && _pos.y < _matrix.GetLength(1);
        }

        public static bool IsEmpty<T>(this IList<T> _list)
        {
            return _list == null || _list.Count == 0;
        }

        public static T Random<T>(this ICollection<T> _list)
        {
            if (_list.Count == 0)
                throw new IndexOutOfRangeException("Cannot select a random item from an empty list !");

            return _list.ElementAt(UnityEngine.Random.Range(0, _list.Count));
        }

        public static List<T> RemoveList<T>(this List<T> _listA, List<T> _listB)
        {
            if (_listA.GetType() != _listB.GetType()) return null;

            foreach (T t in _listB)
                _listA.Remove(t);

            return _listA;
        }

        public static List<T> RemoveList<T>(this List<T> _listA, T[] _listB)
        {
            if (_listA.GetType() != _listB.GetType()) return null;

            foreach (T t in _listB)
                _listA.Remove(t);

            return _listA;
        }

        public static T[] Add<T>(this T[] _array, T _addValue)
        {
            T[] newArray = new T[_array.Length + 1];
            for (int i = 0; i < _array.Length; i++)
                newArray[i] = _array[i];

            newArray[_array.Length] = _addValue;
            return newArray;
        }

        public static List<T2> ChangeToComponentList<T1, T2>(this List<T1> _oldList) where T1 : Behaviour
        {
            List<T2> newList = new List<T2>();

            foreach (T1 g in _oldList)
            {
                T2 t2 = g.GetComponentInChildren<T2>();
                if (t2 != null)
                    newList.Add(t2);
            }

            return newList;
        }

        public static T[] ClearDuplicate<T>(this T[] _array)
        {
            List<T> newList = new List<T>();
            foreach (var t in _array)
            {
                if (!newList.Contains(t))
                    newList.Add(t);
            }

            return newList.ToArray();
        }

        public static List<T> ClearDuplicate<T>(this List<T> _list)
        {
            List<T> newList = new List<T>();
            foreach (var t in _list)
            {
                if (!newList.Contains(t))
                    newList.Add(t);
            }

            return newList;
        }

        public static T[,] ToMatrix<T>(this List<T> _list, int _maxWidth = 5)
        {
            int width = _list.Count <= _maxWidth ? _list.Count : _maxWidth;
            int height = Mathf.CeilToInt(_list.Count / _maxWidth);

            T[,] matrix = new T[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int index = _maxWidth * j + i;

                    if (index == _list.Count)
                        return matrix;

                    matrix[j, i] = _list[index];
                }
            }

            return matrix;
        }

        public static T ClosestObject<T>(this IEnumerable<T> _points, Vector3 _target) where T : Behaviour
        {
            float distance = float.MaxValue;
            T closestPoint = null;

            foreach (var point in _points)
            {
                float d = Vector3.Distance(point.transform.position, _target);
                if (d < distance)
                {
                    distance = d;
                    closestPoint = point;
                }
            }

            return closestPoint;
        }
    }
}