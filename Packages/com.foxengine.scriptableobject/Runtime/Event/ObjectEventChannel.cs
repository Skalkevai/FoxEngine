using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannel/Object")]
public class ObjectEventChannel : SerializedSO
{
    [SerializeField] private string description;
    public event Action<object> OnEventExecuted;

    public void ExecuteEvent(object _obj)
    {
        if (OnEventExecuted != null)
            OnEventExecuted.Invoke(_obj);
    }
}
