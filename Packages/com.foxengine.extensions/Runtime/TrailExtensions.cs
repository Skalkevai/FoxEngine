using System.Collections;
using UnityEngine;

namespace FoxEngine
{
    public static class TrailExtensions
    {
        public static void Reset(this TrailRenderer _trail, MonoBehaviour _instance)
        {
            _instance.StartCoroutine(ResetTrail(_trail));   
        }
        
        static IEnumerator ResetTrail(TrailRenderer _trail)
        {
            float trailTime = _trail.time;
            _trail.time = 0;
            yield return new WaitForEndOfFrame();
            _trail.time = trailTime;
        }      
    }
}