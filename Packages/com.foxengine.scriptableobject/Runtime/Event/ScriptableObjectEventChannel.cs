using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannel/ScriptableObject")]
public class ScriptableObjectEventChannel : SerializedSO
{
    [SerializeField] private string description;
    public event Action<ScriptableObject> OnEventExecuted;

    public void ExecuteEvent(ScriptableObject _so)
    {
        if (OnEventExecuted != null)
            OnEventExecuted.Invoke(_so);
    }
}
