using UnityEngine;
using UnityEngine.Events;

public class ScriptableObjectEventListener : MonoBehaviour
{
    [SerializeField]
    private ScriptableObjectEventChannel channel = default;
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

    private void Respond(ScriptableObject _obj)
    {
        if (onEventExecuted != null)
            onEventExecuted.Invoke(_obj);
    }
}
