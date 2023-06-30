using System;
using System.Collections.Generic;
using UnityEngine;

namespace FoxEngine
{

    public class Sensor2D : MonoBehaviour
    {
        [SerializeField] private LayerMask layerToDetect          = default;
        [SerializeField] private GameObject closest               = default;
        [SerializeField] private List<GameObject> objectsDetected = new List<GameObject>();
        private CircleCollider2D cc;

        public CircleCollider2D GetCollider => cc;

        private void Awake()
        {
            cc = GetComponent<CircleCollider2D>();
            cc.isTrigger = true;
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
            cc.radius = _radius;
        }

        public void ChangeLayerToDetect(LayerMask _layerMask)
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
            float closestDistance = 999;
            GameObject target = null;

            foreach (GameObject o in objectsDetected)
            {
                if (!o) continue;

                float distance = Vector2.Distance(o.transform.position, transform.position);
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
            float closestDistance = 999;
            T target = default;

            foreach (GameObject o in objectsDetected)
            {
                float distance = Vector2.Distance(o.transform.position, transform.position);
                if (o.GetComponent<T>() != null && distance < closestDistance)
                {
                    closestDistance = distance;
                    target = o.GetComponent<T>();
                }
            }

            return target;
        }

        public void OnTriggerStay2D(Collider2D _collider)
        {
            if (!_collider.gameObject.IsInLayerMask(layerToDetect))
                return;

            if (!objectsDetected.Contains(_collider.gameObject))
                objectsDetected.Add(_collider.gameObject);
        }

        public void OnTriggerExit2D(Collider2D _collider)
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
