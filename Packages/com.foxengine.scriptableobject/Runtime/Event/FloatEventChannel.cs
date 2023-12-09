using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannel/Float")]
public class FloatEventChannel : SerializedSO
{
    [SerializeField] private string description;
    public event Action<float> OnEventExecuted;

    public void ExecuteEvent(float _nb)
    {
        if (OnEventExecuted != null)
            OnEventExecuted.Invoke(_nb);
    }
}