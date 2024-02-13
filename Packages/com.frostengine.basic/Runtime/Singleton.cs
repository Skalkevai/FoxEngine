using System;
using UnityEngine;

namespace Frost
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static Action OnBeforeInitialized;
        public static Action OnInitialized;
        public static Action OnStop;
        
        private static T instance = default;
        public static bool isClosed = false;
        public static bool isInitialized = false;
        
        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    string file = typeof(T).ToString();
                    string[] split = file.Split('.');
                    file = split[split.Length-1];
                    
                    var prefab = Resources.Load<T>($"{file}");
                    if (prefab != null)
                        instance = Instantiate(Resources.Load<T>($"{file}"));
                    else
                        new GameObject(file).AddComponent<T>();
                }

                return instance;
            }
        }
        
        public virtual void Awake()
        {
            if (instance)
                Destroy(gameObject);
            else
            {
                isClosed = false;
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }

            Init();
        }

        protected virtual void Init()
        {
            OnBeforeInitialized?.Invoke();
            OnInitialized?.Invoke();
            isInitialized = true;
        }
        
        public virtual void Stop()
        {
            isInitialized = false;
            isClosed = true;
            OnStop?.Invoke();
        }

        protected virtual void OnDisable()
        {
            Stop();
        }

        public void ForceInit() {}
    }
}