using System;

namespace Frost
{
    [Serializable]
    public struct Optional<T>
    {
        public bool enabled;
        public T value;

        public Optional(T _value,bool _enabled = true)
        {
            enabled = _enabled;
            value = _value;
        }
        
        public Optional(bool _enabled)
        {
            value = default;
            enabled = _enabled;
        }
    }
}