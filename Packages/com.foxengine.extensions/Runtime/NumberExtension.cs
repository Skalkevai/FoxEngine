using UnityEngine;
namespace FoxEngine
{
	public static class FloatExtension
	{
		public static float AtLeast (this float _v, float _min)
		{
			return Mathf.Max(_v, _min);
		}
		
		public static float AtMost (this float _v, float _max)
		{
			return Mathf.Min(_v, _max);
		}
	}
	
	public static class IntExtension
	{
        public static bool IsPair(this int _v)
        {
            return _v % 2 == 0;
        }

        public static int AtLeast (this int _v, int _min)
		{
			return Mathf.Max(_v, _min);
		}
		
		public static int AtMost (this int _v, int _max)
		{
			return Mathf.Min(_v, _max);
		}
	}
}
