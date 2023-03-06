using UnityEngine;

namespace FoxEngine
{
    public static class BoundsExtension
    {

        public static Vector2 RandomPosition_V2(this Bounds _bounds)
        {
            return new Vector2() 
            {
                x = Random.Range(_bounds.min.x,_bounds.max.x),
                y = Random.Range(_bounds.min.y, _bounds.max.y)
            };
        }

        public static Vector3 RandomPosition_V3(this Bounds _bounds)
        {
            return new Vector3()
            {
                x = Random.Range(_bounds.min.x, _bounds.max.x),
                y = Random.Range(_bounds.min.y, _bounds.max.y),
                z = Random.Range(_bounds.min.z, _bounds.max.z)
            };
        }
    }
}