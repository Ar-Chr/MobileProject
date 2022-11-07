using UnityEngine;

namespace Ball.Shared.Utility
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; set; }

        protected void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                Debug.LogError("[Singleton] Trying to instantiate a second instance of a singleton class");
            }
            else
            {
                Instance = (T)this;
            }
        }

        protected void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}