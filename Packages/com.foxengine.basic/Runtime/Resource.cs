using System;
using Unity.Collections;
using UnityEngine;

namespace FoxEngine
{
    [Serializable]
    public class Counter<T>
    {
        public T max = default;
        public T min = default;
        public T current = default;

        public Counter()
        {
            Init();
        }

        public void Init(bool _reverse = false)
        {
            if (_reverse)
                current = default;
            else
                current = max;
        }
    }

    [Serializable]
    public class Resource
    {
        [SerializeField] private string name = "Resource";
        [SerializeField] private Counter<float> value;

        public float Value => value.current;
        public float min => value.min;
        public float max => value.max;

        public Resource()
        {
            value = new Counter<float>();
            value.min = 0;
            value.max = float.MaxValue;
        }

        public Resource(float _current)
        {
            value = new Counter<float>();
            value.current = _current;
            value.min = 0;
            value.max = float.MaxValue;
        }

        public Resource(float _current, float _min, float _max)
        {
            value = new Counter<float>
            {
                current = _current,
                min = _min,
                max = _max
            };
        }

        public Resource(Resource _r)
        {
            value = new Counter<float>
            {
                current = _r.Value,
                min = _r.min,
                max = _r.max
            };
        }

        public Resource(Counter<float> _value)
        {
            value = _value;
        }

        public bool TrySpend(float _amount)
        {
            if (value.current >= _amount)
            {
                value.current -= _amount;
                return true;
            }

            return false;
        }

        public void Add(float _amount)
        {
            value.current = Mathf.Clamp(value.current + _amount, value.min, value.max);
        }

        public void Remove(float _amount)
        {
            value.current = Mathf.Clamp(value.current - _amount, value.min, value.max);
        }

        public void Clear()
        {
            value.current = 0;
        }

        public bool IsEmpty()
        {
            return value.current <= 0;
        }

        public bool IsFull()
        {
            return value.current == value.max;
        }


        #region Conversion
        public static implicit operator byte(Resource _r) => (byte)_r.Value;
        public static explicit operator Resource(byte _b) => new Resource(_b);

        public static implicit operator float(Resource _r) => (float)_r.Value;
        public static explicit operator Resource(float _f) => new Resource(_f);

        public static implicit operator int(Resource _r) => (int)_r.Value;
        public static explicit operator Resource(int _i) => new Resource(_i);

        public static implicit operator uint(Resource _r) => (uint)_r.Value;
        public static explicit operator Resource(uint _i) => new Resource(_i);

        public static implicit operator ulong(Resource _r) => (ulong)_r.Value;
        public static explicit operator Resource(ulong _i) => new Resource(_i);

        public static implicit operator ushort(Resource _r) => (ushort)_r.Value;
        public static explicit operator Resource(ushort _i) => new Resource(_i);

        public static implicit operator short(Resource _r) => (short)_r.Value;
        public static explicit operator Resource(short _i) => new Resource(_i);

        public static implicit operator long(Resource _r) => (long)_r.Value;
        public static explicit operator Resource(long _i) => new Resource(_i);
        #endregion

        #region Operator
        public static Resource operator -(Resource _a)
        {
            return new Resource(-_a.Value, _a.min, _a.max);
        }

        public static Resource operator +(Resource _r1, Resource _r2)
        {
            Resource temp = new Resource(_r1);
            temp.Add(_r2.Value);

            return temp;
        }

        public static Resource operator +(Resource _r1, float _f)
        {
            Resource temp = new Resource(_r1);
            temp.Add(_f);

            return temp;
        }

        public static Resource operator -(Resource _r1, float _f)
        {
            Resource temp = new Resource(_r1);
            temp.Remove(_f);

            return temp;
        }

        public static Resource operator ++(Resource _r1)
        {
            Resource temp = new Resource(_r1);
            temp.Add(1);

            return temp;
        }

        public static Resource operator --(Resource _r1)
        {
            Resource temp = new Resource(_r1);
            temp.Remove(1);

            return temp;
        }

        public static Resource operator *(Resource _r1, Resource _r2)
        {
            Resource temp = new Resource(_r1);
            temp.value.current *= _r2.Value;

            return temp;
        }

        public static Resource operator *(Resource _r1, float _f)
        {
            Resource temp = new Resource(_r1);
            temp.value.current *= _f;

            return temp;
        }

        public static Resource operator /(Resource _r1, float _f)
        {
            Resource temp = new Resource(_r1);
            temp.value.current /= _f;

            return temp;
        }

        public static Resource operator %(Resource _r1, float _f)
        {
            Resource temp = new Resource(_r1);
            temp.value.current %= _f;

            return temp;
        }

        public static Resource operator /(Resource _r1, Resource _r2)
        {
            Resource temp = new Resource(_r1);
            temp.value.current /= _r2.Value;

            return temp;
        }

        public static Resource operator %(Resource _r1, Resource _r2)
        {
            Resource temp = new Resource(_r1);
            temp.value.current %= _r2.Value;

            return temp;
        }

        public override bool Equals(object o)
        {
            if (o is Resource)
            {
                if (((Resource)o).Value == Value)
                    return true;
            }
            else if (o is float)
            {
                if ((float)o == Value)
                    return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{name} \n\tValue : {value} \n\tMax : {max} \n\tMin : {min}";
        }

        public static bool operator ==(Resource _r1, Resource _r2)
        {
            return _r1.Equals(_r2);
        }

        public static bool operator ==(Resource _r1, float _f)
        {
            return _r1.Equals(_f);
        }

        public static bool operator !=(Resource _r1, float _f)
        {
            return !_r1.Equals(_f);
        }

        public static bool operator !=(Resource _r1, Resource _r2)
        {
            return !_r1.Equals(_r2);
        }

        public static bool operator > (Resource _r1, Resource _r2) 
        {
            return _r1.Value > _r2.Value;
        }

        public static bool operator >(Resource _r1, float _f)
        {
            return _r1.Value > _f;
        }

        public static bool operator <(Resource _r1, float _f)
        {
            return _r1.Value < _f;
        }

        public static bool operator >=(Resource _r1, float _f)
        {
            return _r1.Value >= _f;
        }

        public static bool operator <=(Resource _r1, float _f)
        {
            return _r1.Value <= _f;
        }

        public static bool operator <(Resource _r1, Resource _r2)
        {
            return _r1.Value < _r2.Value;
        }

        public static bool operator >=(Resource _r1, Resource _r2)
        {
            return _r1.Value >= _r2.Value;
        }

        public static bool operator <=(Resource _r1, Resource _r2)
        {
            return _r1.Value <= _r2.Value;
        }

        #endregion
    }
}