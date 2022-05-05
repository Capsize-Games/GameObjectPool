using UnityEngine;

namespace ObjectPool.Scripts
{
    [System.Serializable]
    public class PoolSettings
    {
        public string name;
        public GameObject prefab;
        public int startingItemCount;
        public int maxItemCount;
        public Transform parent;
        public bool allowUnrestrictedGrowth = false;
        public bool showAnalytics;
    }
}
