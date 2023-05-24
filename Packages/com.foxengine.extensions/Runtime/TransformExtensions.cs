using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public static class TransformExtensions
{
    public static IEnumerator MoveTo(this Transform _transform, Vector3 _target, float _duration, Action _callback = null)
    {
        _transform.GetComponent<Renderer>()?.material?.EnableKeyword("SHAKEUV_ON");
        Vector3 startPos = _transform.position;
        float timer = 0;
        while (timer < _duration)
        {
            float lerpValue = timer / _duration;
            timer += Time.deltaTime;
            _transform.position = Vector3.Lerp(startPos, _target, lerpValue);
            yield return null;
        }
        _transform.position = _target;

        _transform.GetComponent<Renderer>()?.material?.DisableKeyword("SHAKEUV_ON");

        _callback?.Invoke();
    }
    
    public static void ClearChilds(this Transform _transform)
    {
        int nb = _transform.childCount;
        for (int i = nb - 1; i >= 0; i--)
            Object.Destroy(_transform.GetChild(i).gameObject);
    }
    
    public static void Reset(this Transform _transform)
    {
        _transform.localPosition = Vector3.zero;
        _transform.localRotation = Quaternion.identity;
        _transform.localScale = Vector3.one;
    }
}
