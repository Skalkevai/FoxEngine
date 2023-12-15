using System.Collections.Generic;
namespace FoxEngine
{
    using System;
    using UnityEngine;

    [Serializable]
    public class MinMax
    {
        [SerializeField] private float min = default;
        [SerializeField] private float max = default;

        public void OnValidate()
        {
            if (min > max)
                (min, max) = (max, min);
        }

        public float Min
        {
            get => min;

            set
            {
                if (value > min)
                    Debug.LogWarning($"Warning: min should be less than or equal to max! New MinMax : ({value}, {max})");

                min = value;
            }
        }

        public float Max
        {
            get => max;
        
            set
            {
                if (value < min)
                    Debug.LogWarning($"Warning: max should be greater than or equal to min! New MinMax : ({min}, {value})");

                max = value;
            }
        }

        public float Random()
        {
            return UnityEngine.Random.Range(Min,Max);
        }

        public int RandomInt()
        {
            return UnityEngine.Random.Range((int)Min,(int)Max+1);
        }
    
        public MinMax()
        {
            min = 0;
            max = 0;
        }
    
        public MinMax(float a, float b)
        {
            SetMinMax(a, b);
        }

        public void SetMinMax(float _a, float _b)
        {
            if (_a > _b)
                (_a, _b) = (_b, _a);

            min = _a;
            max = _b;
        }
    }

}