using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannel/Int")]
public class IntEventChannel : SerializedSO
{
    [SerializeField] private string description;
    public event Action<int> OnEventExecuted;

    public void ExecuteEvent(int _nb)
    {
        if (OnEventExecuted != null)
            OnEventExecuted.Invoke(_nb);
    }
}