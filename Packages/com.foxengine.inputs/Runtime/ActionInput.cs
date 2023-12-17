using UnityEngine;

namespace FoxEngine
{
    [CreateAssetMenu]
    public class ActionInput : SerializedSO
    {
        public string key;
        public ControlsPair[] controls;
    }
}