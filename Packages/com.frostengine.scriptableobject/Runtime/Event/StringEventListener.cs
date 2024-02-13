using UnityEngine;
using UnityEngine.Events;

public class StringEventListener : MonoBehaviour
{
    [SerializeField]
    private StringEventChannel channel = default;
    public UnityEvent<string> onEventExecuted;

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

    private void Respond(string _text)
    {
        if (onEventExecuted != null)
            onEventExecuted.Invoke(_text);
    }
}
