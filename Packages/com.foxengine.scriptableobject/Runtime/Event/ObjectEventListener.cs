using UnityEngine;
using UnityEngine.Events;

public class ObjectEventListener : MonoBehaviour
{
    [SerializeField]
    private ObjectEventChannel channel = default;
    public UnityEvent<object> onEventExecuted;

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

    private void Respond(object _obj)
    {
        if (onEventExecuted != null)
            onEventExecuted.Invoke(_obj);
    }
}
