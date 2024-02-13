using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannel/String")]
public class StringEventChannel : SerializedSO
{
    [SerializeField] private string description;
    public event Action<string> OnEventExecuted;

    public void ExecuteEvent(string _text)
    {
        if (OnEventExecuted != null)
            OnEventExecuted.Invoke(_text);
    }
}