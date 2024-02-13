using UnityEngine;
using UnityEngine.Events;

public class FloatEventListener : MonoBehaviour
{
    [SerializeField]
    private FloatEventChannel channel = default;
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

    private void Respond(float _nb)
    {
        if (onEventExecuted != null)
            onEventExecuted.Invoke(_nb);
    }
}
