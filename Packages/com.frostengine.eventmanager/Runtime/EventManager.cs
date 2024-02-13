using System;
using System.Collections.Generic;

namespace Frost
{
    public class EventManager : Singleton<EventManager>
    {
        private Dictionary<string, Action<object>> eventDict = default;
        private Dictionary<string, Action<object,object>> eventDict2 = default;

        public override void Awake()
        {
            base.Awake();
            eventDict = new Dictionary<string, Action<object>>();
            eventDict2 = new Dictionary<string, Action<object,object>>();
        }

        public void RegisterEvent(string _key, Action<object> _callback)
        {
            if (!isInitialized)
            {
                OnInitialized += () => RegisterEvent(_key, _callback);
                return;
            }

            if (eventDict.ContainsKey(_key))
                eventDict[_key] += _callback;
            else
                eventDict.Add(_key, _callback);
        }
        
        public void RegisterEvent(string _key, Action<object,object> _callback)
        {
            if (!isInitialized)
            {
                OnInitialized += () => RegisterEvent(_key, _callback);
                return;
            }

            if (eventDict2.ContainsKey(_key))
                eventDict2[_key] += _callback;
            else
                eventDict2.Add(_key, _callback);
        }

        public void UnregisterEvent(string _key, Action<object> _callback)
        {
            if (!isInitialized)
            {
                OnInitialized += () => UnregisterEvent(_key, _callback);
                return;
            }

            if (eventDict.ContainsKey(_key))
                eventDict[_key] -= _callback;
        }

        public void UnregisterEvent(string _key, Action<object,object> _callback)
        {
            if (!isInitialized)
            {
                OnInitialized += () => UnregisterEvent(_key, _callback);
                return;
            }

            if (eventDict2.ContainsKey(_key))
                eventDict2[_key] -= _callback;
        }

        public void ExecuteEvents(string _key, object _param = null)
        {
            if (!isInitialized)
            {
                OnInitialized += () => ExecuteEvents(_key, _param);
                return;
            }

            if (eventDict.ContainsKey(_key))
                eventDict[_key]?.Invoke(_param);
            else
                Debug.LogWarning("The EventID isn't register");
        }
        
        public void ExecuteEvents(string _key, object _param1,object _param2)
        {
            if (!isInitialized)
            {
                OnInitialized += () => ExecuteEvents(_key, _param1, _param2);
                return;
            }

            if (eventDict2.ContainsKey(_key))
                eventDict2[_key]?.Invoke(_param1,_param2);
            else
                Debug.LogWarning("The EventID isn't register");
        }
        
        public void OnDestroy()
        {
            eventDict.Clear();
            eventDict2.Clear();
        }
    }
}