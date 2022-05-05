using System;
using UnityEngine;

namespace ObjectPool.Scripts
{
    [Serializable]
    public class PoolSettings
    {
        public string name;
        public GameObject prefab;
        public int startingItemCount;
        public int maxItemCount;
        public Transform parent;
        public bool allowUnrestrictedGrowth;
        public bool showAnalytics;
    }
}