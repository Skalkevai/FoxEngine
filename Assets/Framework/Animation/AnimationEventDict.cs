using UnityEngine;
using UnityEngine.Events;

namespace FoxEngine
{
	public class AnimationEventDict : MonoBehaviour
	{
		[SerializeField] private FoxDictionary<string,UnityEvent> actions = new FoxDictionary<string, UnityEvent>();

		//Called in button Action
		public void ExecuteAction(string _index)
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
