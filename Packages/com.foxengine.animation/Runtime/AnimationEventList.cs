using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FoxEngine
{
    public class AnimationEventList : MonoBehaviour
    {
        
        [SerializeField] private List<UnityEvent> actions = default;

        //Called in button Action
        public void ExecuteAction(int _index)
        {
            actions[_index]?.Invoke();
        }

        public new void DestroyObject(Object _obj)
        {
            if(_obj == null)
                Destroy(gameObject);
            else 
                Destroy(_obj);
        }
    }
}