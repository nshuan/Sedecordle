using UnityEngine;

namespace Core.Singleton
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        public static bool IsNull => _instance == null;
        protected static T _instance;
    
        /// <summary>
        /// Singleton design pattern
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        _instance = obj.AddComponent<T>();
                    }
                }
    
                return _instance;
            }
        }
    
        /// <summary>
        /// On awake, we initialize our instance. Make sure to call base.Awake() in override if you need awake.
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                return;
            }
    
            DestroyImmediate(this);
        }
    
        private void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }
    }
}