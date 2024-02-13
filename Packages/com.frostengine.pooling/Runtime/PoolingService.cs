using System.Collections.Generic;
using UnityEngine;

namespace Frost
{
    public class PoolingManager : Singleton<PoolingManager>
    {
        [Title("Debug")]
        [SerializeField] private bool showLogs                  = default;

        [Title("Pooling")]
        [SerializeField] private int nbDefault                  = 5;
        [SerializeField] private PoolList poolAtStart           = default;
        private Dictionary<string,Pooling> poolings             = new Dictionary<string,Pooling>();

        public override void Awake()
        {
            base.Awake();
            if(poolAtStart != null)
                CreatePools(poolAtStart);
        }

        public void CreatePools(PoolList _poolList)
        {
            foreach (var item in _poolList.items.Dictionary)
            {
                if (item.Key == null)
                    continue;

                if (!poolings.ContainsKey(item.Key.name))
                    CreatePooling(item.Key,item.Value);
            }
        }

        public PoolItem SpawnItem<T>(T _poolItem) where T : PoolItem
        {
            return SpawnItem(_poolItem,Vector3.zero);
        }

        public PoolItem SpawnItem<T>(T _poolItem,Vector3 _position) where T : PoolItem
        {
            if (!_poolItem)
                return null;

            if (!poolings.ContainsKey(_poolItem.name))
                if (!CreatePooling(_poolItem))
                    return null;

            PoolItem poolItem = poolings[_poolItem.name].GetPoolItem(_poolItem.CanIncreasePool);
            if (!poolItem)
                return null;

            poolItem.SetPosition(_position);
            return poolItem;
        }

        public bool CreatePooling(PoolItem _poolItem,int _nb = -1)
        {
            if (!_poolItem)
            {
                Debug.LogError($"{_poolItem.name} is not a pooling item !");
                return false;
            }

            GameObject poolingObject = new GameObject($"Pooling {_poolItem.name}");
            poolingObject.transform.parent = transform;
            
            Pooling pooling = poolingObject.AddComponent<Pooling>();
            if(showLogs)
                Debug.LogNotImportant($"Pooling [{_poolItem.name}] created");
            pooling.SetPoolItem(_poolItem);

            pooling.SetNumberDefault((_nb==-1)?nbDefault:_nb);
            
            poolings.Add(_poolItem.name,pooling);

            return true;
        }
        
        public Transform GetPoolingLocation()
        {
            return transform;
        }
    }
}