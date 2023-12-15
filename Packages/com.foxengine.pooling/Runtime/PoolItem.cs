using System;
using System.Collections;
using UnityEngine;

namespace FoxEngine
{
    [Serializable]
    public class PoolItem : MonoBehaviour
    {
        [SerializeField] private bool canIncreasePool;

        protected Action<PoolItem> onRemove = default;
        protected ParticleSystem itemParticleSystem;
        protected Material mat;
        protected TrailRenderer trail;
        protected Rigidbody rb;
        protected Rigidbody2D rb2d;

        public Material Material => mat;
        public ParticleSystem ItemParticleSystem => itemParticleSystem;
        public bool CanIncreasePool => canIncreasePool;
        
        public virtual void Awake ()
        {
            itemParticleSystem = GetComponentInChildren<ParticleSystem>();
            rb = GetComponentInChildren<Rigidbody>();
            rb2d = GetComponentInChildren<Rigidbody2D>();
            
            if(TryGetComponent(out Renderer r))
                mat = r.material;
            
            if(TryGetComponent(out TrailRenderer t))
                trail = t;
        }

        public void AssignRemove(Action<PoolItem> _callback)
        {
            onRemove += _callback;
            Deactivate();
        }

        public void SetPosition(Vector3 _position)
        {
            transform.position = _position;
        }

        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        public virtual void AddForce(Vector3 _force,ForceMode _forceMode)
        {
            if (rb)
                rb.AddForce(_force, _forceMode);     
        }

        public virtual void AddForce2D(Vector2 _force, ForceMode2D _forceMode)
        {
            if (rb2d)
                rb2d.AddForce(_force, _forceMode);
        }

        public virtual void Deactivate(float _timer = 0)
        {
            if (_timer != 0)
            {
                StartCoroutine(DeactivateDelay(_timer));
                return;
            }

            onRemove?.Invoke(this);
            gameObject.SetActive(false);

            if(trail)
                trail.Clear();
            
            if (TryGetComponent(out Rigidbody rb))
                rb.velocity = Vector3.zero;
            
            if (TryGetComponent(out Rigidbody2D rb2))
                rb2.velocity = Vector2.zero;
        }

        private IEnumerator DeactivateDelay(float _timer)
        {
            yield return new WaitForSeconds(_timer);

            onRemove?.Invoke(this);
            gameObject.SetActive(false);
        }

        public virtual void OnParticleSystemStopped()
        {
            Deactivate();
        }
    }
}
