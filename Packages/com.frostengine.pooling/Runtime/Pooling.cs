using System.Collections.Generic;
using UnityEngine;
using Frost;
using System.Linq;

namespace Frost
{
    public class Pooling : MonoBehaviour
    {
        [SerializeField] private PoolItem prefab = default;
        protected int defaultSize = 0;
        protected List<PoolItem> actives = new List<PoolItem>();
        protected List<PoolItem> inactives = new List<PoolItem>();
        private bool setup = false;

        protected void Awake()
        {
            for (int i = 0; i < defaultSize; i++)
                AddToPool();
            setup = true;
        }

        public PoolItem GetPoolItem(bool _canIncreasePool,bool _activated = true)
        {
            PoolItem item = inactives.Find(poolItem => poolItem);
            if (!item)
            {
                if (_canIncreasePool)
                    item = AddToPool();
                else
                {
                    Debug.LogWarning("No more item available, will recycle an active item");
                    PoolItem i = actives.First();
                    i.Deactivate();
                    return GetPoolItem(_canIncreasePool,_activated);
                }
                return null;
            }

            inactives.Remove(item);
            actives.Add(item);
            if (_activated)
                item.Activate();

            return item;
        }

        private PoolItem AddToPool()
        {
            PoolItem item = Instantiate(prefab, transform);
            item.AssignRemove(OnRemoveCallback);

            return item;
        }

        private void OnRemoveCallback(PoolItem item)
        {
            actives.Remove(item);
            inactives.Add(item);
        }

        public void SetPoolItem(PoolItem _poolItem)
        {
            prefab = _poolItem;
        }

        public void DesactiveAll()
        {
            PoolItem[] items = actives.ToArray();
            foreach (PoolItem poolItem in items)
                poolItem.Deactivate();
        }

        public void SetNumberDefault(int _nbDefault)
        {
            defaultSize = _nbDefault;
            if (setup)
                Awake();
        }
    }
}