using UnityEngine;
using UnityEngine.Events;

namespace Frost
{
	public class AnimationEventDict : MonoBehaviour
	{
		[SerializeField] private FrostDictionary<string,UnityEvent> actions = new FrostDictionary<string, UnityEvent>();

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
