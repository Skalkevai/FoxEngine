using System.Collections.Generic;
using UnityEngine;

namespace FoxEngine
{
    public class PoolingManager : Singleton<PoolingManager>
    {
        [SerializeField] private bool showLogs                  = default;
        [SerializeField] private int nbDefault                  = default;
        [SerializeField] private Transform poolingLocation      = default;
        private Dictionary<string,Pooling> poolings             = new Dictionary<string,Pooling>();

        public override void Awake()
        {
            base.Awake();
            if (!poolingLocation)
                poolingLocation = transform;
        }

        public PoolItem SpawnItem(GameObject _poolItem)
        {
            if (!_poolItem)
                return null;

            if (!poolings.ContainsKey(_poolItem.name))
                if (!CreatePooling(_poolItem))
                    return null;

            return poolings[_poolItem.name].GetPoolItem();
        }
        
        public PoolItem SpawnItem<T>(T _poolItem) where T : MonoBehaviour
        {
            if (!_poolItem)
                return null;

            if (!poolings.ContainsKey(_poolItem.name))
                if (!CreatePooling(_poolItem.gameObject))
                    return null;

            return poolings[_poolItem.name].GetPoolItem();
        }

        public bool CreatePooling(GameObject _poolItem)
        {
            PoolItem poolItem = _poolItem.GetComponent<PoolItem>();
            if (!poolItem)
            {
                Debug.DebugError($"{_poolItem.name} is not a pooling item !");
                return false;
            }

            GameObject poolingObject = new GameObject($"Pooling {_poolItem.name}");
            poolingObject.transform.parent = poolingLocation;
            
            Pooling pooling = poolingObject.AddComponent<Pooling>();
            if(showLogs)
                Debug.DebugNotImportantLog($"Pooling [{_poolItem.name}] created");
            pooling.SetPoolItem(poolItem);
            pooling.SetNumberDefault(nbDefault);
            
            poolings.Add(_poolItem.name,pooling);

            return true;
        }
        
        public void SetPoolingLocation(Transform _transform)
        {
            poolingLocation = _transform;
        }

        public Transform GetPoolingLocation()
        {
            return poolingLocation;
        }
    }
}