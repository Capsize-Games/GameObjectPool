using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool.Scripts
{
    public class PoolManager : MonoBehaviour
    {
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
            if (showAnalytics)
                for (var i = 0; i < m_pool.Count; i++)
                    if (m_pool[i].showAnalytics)
                    {
                        var name = m_pool[i].name;
                        GUI.Label(
                            new Rect(10, 10 + i * 40, 400, 20),
                            name
                        );
                        GUI.Label(
                            new Rect(10, 30 + i * 40, 400, 20),
                            "Available in Pool: " + TotalInPool(name) + " Active: " + TotalActive(name)
                        );
                    }
        }

        public static void AddPool(PoolSettings settings)
        {
            PoolSettings.Add(settings);
            Pools.Add(settings.name, new Pool(settings));
        }

        private static void PopulatePools()
        {
            foreach (var settings in PoolSettings)
                if (!Pools.ContainsKey(settings.name))
                    Pools.Add(settings.name, new Pool(settings));
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

        public static GameObject Get(string poolName, Vector3 position)
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

        public Pool GetPool(string poolName)
        {
            return Pools[poolName];
        }

        public static int TotalInPool(string poolName)
        {
            var pool = Instance.GetPool(poolName);
            if (pool != null) return pool.TotalInPool;
            return 0;
        }

        public static int TotalActive(string poolName)
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

            foreach (var settings in m_pool)
                if (!Pools.ContainsKey(settings.name))
                    Pools.Add(settings.name, new Pool(settings));
        }

        #region Fields

        private const string ERROR_MISSING_MANAGER = "PoolManager script required.";
        public bool debug = true;
        public List<PoolSettings> m_pool = new();
        public bool showAnalytics;
        private Dictionary<string, Pool> pools;

        #endregion

        #region Properties

        public static List<PoolSettings> PoolSettings => Instance.m_pool;

        private static PoolManager Instance { get; set; }

        public static Dictionary<string, Pool> Pools
        {
            get => Instance.pools ?? (Instance.pools = new Dictionary<string, Pool>());
            private set => Instance.pools = value;
        }

        #endregion
    }
}