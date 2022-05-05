using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool.Scripts
{
    public class PoolManager : MonoBehaviour
    {
        #region Fields
        public List<PoolSettings> mPool = new();
        public bool showAnalytics;
        private Dictionary<string, Pool> _pools;
        #endregion

        #region Properties
        private static PoolManager Instance { get; set; }
        private static Dictionary<string, Pool> Pools
        {
            get => Instance._pools ?? (Instance._pools = new Dictionary<string, Pool>());
            set => Instance._pools = value;
        }
        #endregion

        private void Start()
        {
            if (Instance == null) Instance = this;
            GeneratePools(false);
        }

        /// <summary>
        ///     Display information about a PoolManager
        /// </summary>
        private void OnGUI()
        {
            if (!showAnalytics) return;
            for (var i = 0; i < mPool.Count; i++)
            {
                if (!mPool[i].showAnalytics) continue;
                var poolName = mPool[i].name;
                GUI.Label(
                    new Rect(10, 10 + i * 40, 400, 20),
                    poolName
                );
                GUI.Label(
                    new Rect(10, 30 + i * 40, 400, 20),
                    "Available in Pool: " + TotalInPool(poolName) + " Active: " + TotalActive(poolName)
                );
            }
        }

        public static GameObject Get(string poolName, Vector3 position, Quaternion rotation, Transform parent)
        {
            var obj = Get(poolName, position, rotation);
            if (obj) obj.transform.SetParent(parent);
            return obj;
        }

        public static GameObject Get(string poolName, Vector3 position, Quaternion rotation)
        {
            var obj = Get(poolName, position);
            if (obj) obj.transform.rotation = rotation;
            return obj;
        }

        private static GameObject Get(string poolName, Vector3 position)
        {
            var obj = Get(poolName);
            if (obj) obj.transform.position = position;
            return obj;
        }

        public static GameObject Get(string poolName, Transform transform)
        {
            var obj = Get(poolName);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            return obj;
        }

        public static GameObject Get(string poolName)
        {
            if (Pools.ContainsKey(poolName)) return Instance.GetPool(poolName).Get;
            return null;
        }

        public static GameObject[] Get(string poolName, int total)
        {
            var objects = new GameObject[total];
            for (var i = 0; i < total; i++) objects[i] = Instance.GetPool(poolName).Get;
            return objects;
        }

        private Pool GetPool(string poolName)
        {
            return Pools[poolName];
        }

        private static int TotalInPool(string poolName)
        {
            var pool = Instance.GetPool(poolName);
            if (pool != null) return pool.TotalInPool;
            return 0;
        }

        private static int TotalActive(string poolName)
        {
            var pool = Instance.GetPool(poolName);
            if (pool != null) return pool.TotalActive;
            return 0;
        }

        public void GeneratePools(bool doDestroy = true)
        {
            if (Instance == null) Instance = this;

            var objects = FindObjectsOfType<GameObject>(true);
            if (doDestroy)
                foreach (var obj in objects)
                    if (obj.GetComponent<PooledItem>() != null)
                        DestroyImmediate(obj);
            Pools = new Dictionary<string, Pool>();

            foreach (var settings in mPool)
                if (!Pools.ContainsKey(settings.name))
                    Pools.Add(settings.name, new Pool(settings));
        }
    }
}