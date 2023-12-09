using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VoidEventListener : MonoBehaviour
{
    [SerializeField]
    private VoidEventChannel channel = default;
    public UnityEvent onEventExecuted;

    private void OnEnable()
    {
        if (channel != null)
            channel.OnEventExecuted += Respond;
    }

    private void OnDisable()
    {
        if (channel != null)
            channel.OnEventExecuted -= Respond;
    }

    private void Respond()
    {
        if (onEventExecuted != null)
            onEventExecuted.Invoke();
    }
}
