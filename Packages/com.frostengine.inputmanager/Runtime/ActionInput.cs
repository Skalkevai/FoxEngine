using UnityEngine;

namespace Frost
{
    [CreateAssetMenu]
    public class ActionInput : SerializedSO
    {
        public string key;
        public ControlsPair[] controls;
    }
}