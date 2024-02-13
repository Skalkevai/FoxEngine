using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frost
{

    public class Sensor3D : MonoBehaviour
    {
        [SerializeField] private LayerMask layerToDetect          = default;
        [SerializeField] private GameObject closest               = default;
        [SerializeField] private List<GameObject> objectsDetected = new List<GameObject>();
        private SphereCollider sc;
        private Predicate<Collider> condition;

        private void Awake()
        {
            sc = GetComponent<SphereCollider>();
            if (!sc)
            {
                sc = gameObject.AddComponent<SphereCollider>();
                sc.isTrigger = true;
            }
        }

        private void Update()
        {
            //Delete Empty Space
            CleanEmptySpace();
        }

        public bool HaveTargets()
        {
            return objectsDetected.Count > 0;
        }

        public void ChangeRadius(float _radius)
        {
            sc.radius = _radius;
        }

        public void SetCondition(Predicate<Collider> _condition)
        {
            condition = _condition;
        }

        public void SetLayerMask(LayerMask _layerMask)
        {
            layerToDetect = _layerMask;
        }

        private void CleanEmptySpace()
        {
            for (int i = objectsDetected.Count - 1; i >= 0; i--)
                if (!objectsDetected[i])
                    objectsDetected.Remove(objectsDetected[i]);
        }

        public List<GameObject> GetList()
        {
            return objectsDetected;
        }

        public List<T> GetList<T>()
        {
            List<T> newList = new List<T>();

            foreach (GameObject g in objectsDetected)
                if (g.GetComponent<T>() != null)
                    newList.Add(g.GetComponent<T>());

            return newList;
        }

        public GameObject GetClosestTarget()
        {
            float closestDistance = float.MaxValue;
            GameObject target = null;

            foreach (GameObject o in objectsDetected)
            {
                if (!o) continue;

                float distance = Vector3.Distance(o.transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = o;
                }
            }

            closest = target;
            return closest;
        }

        public T GetClosestTarget<T>()
        {
            float closestDistance = float.MaxValue;
            T target = default;

            foreach (GameObject o in objectsDetected)
            {
                float distance = Vector3.Distance(o.transform.position, transform.position);
                if (o.GetComponent<T>() != null && distance < closestDistance)
                {
                    closestDistance = distance;
                    target = o.GetComponent<T>();
                }
            }

            return target;
        }

        public void OnTriggerStay(Collider _collider)
        {
            if (!_collider.gameObject.IsInLayerMask(layerToDetect))
                return;

            if (condition != null && !condition.Invoke(_collider))
                return;

            if (!objectsDetected.Contains(_collider.gameObject))
                objectsDetected.Add(_collider.gameObject);
        }

        public void OnTriggerExit(Collider _collider)
        {
            if (!_collider.gameObject.IsInLayerMask(layerToDetect))
                return;

            if (objectsDetected.Contains(_collider.gameObject))
                objectsDetected.Remove(_collider.gameObject);
        }

        public LayerMask GetSensorLayer()
        {
            return layerToDetect;
        }
    }
}
