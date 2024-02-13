using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannel/Void")]
public class VoidEventChannel : SerializedSO
{
    [SerializeField] private string description;
    public event Action OnEventExecuted;

    public void ExecuteEvent()
    {
        if (OnEventExecuted != null)
            OnEventExecuted.Invoke();
    }
}