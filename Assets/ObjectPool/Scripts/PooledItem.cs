using UnityEngine;

namespace GameObjectPool
{
    public class PooledItem : MonoBehaviour
    {
        public string poolName = "";

        void OnEnable()
        {
            if (poolName != "")
            {
                PoolManager.Activate(poolName, gameObject);
            }
        }

        void OnDisable()
        {
            PoolManager.Deactivate(poolName, gameObject);
        }
    }
}
