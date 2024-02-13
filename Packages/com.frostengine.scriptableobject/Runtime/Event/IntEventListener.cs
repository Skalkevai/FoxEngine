using UnityEngine;
using UnityEngine.Events;

public class IntEventListener : MonoBehaviour
{
    [SerializeField]
    private IntEventChannel channel = default;
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

    private void Respond(int _nb)
    {
        if (onEventExecuted != null)
            onEventExecuted.Invoke(_nb);
    }
}
