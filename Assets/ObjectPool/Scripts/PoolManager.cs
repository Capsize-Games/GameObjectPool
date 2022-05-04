using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameObjectPool
{
    public class PoolManager : MonoBehaviour
    {
        #region Fields
        const string ERROR_MISSING_MANAGER = "PoolManager script required.";
        public bool debug = true;
        public List<PoolSettings> m_pool = new List<PoolSettings>();
        public bool showAnalytics = false;
        private static List<PoolSettings> deferredPool = new List<PoolSettings>();
        private static PoolManager instance;
        private Dictionary<string, Pool> pools;
        #endregion

        #region Properties
        public static List<PoolSettings> PoolSettings
        {
            get { return Instance.m_pool; }
        }

        private static PoolManager Instance
        {
            get { return instance; }
        }

        public static Dictionary<string, Pool> Pools
        {
            get { return Instance.pools; }
        }
        #endregion

        void Start()
        {
            instance = this;
            pools = new Dictionary<string, Pool>();
            for (int i = 0; i < deferredPool.Count; i++)
            {
                PoolSettings.Add(deferredPool[i]);
            }
            deferredPool.Clear();
            PopulatePools();
        }

        public static void AddPoolDeferred(PoolSettings settings)
        {
            deferredPool.Add(settings);
        }

        public static void AddPool(PoolSettings settings)
        {
            if (Instance)
            {
                PoolSettings.Add(settings);
                Pools[settings.name] = new Pool(settings);
            }
            else
            {
                AddPoolDeferred(settings);
            }
        }

        public static bool HasPool(string poolName)
        {
            return Pools.ContainsKey(poolName);
        }

        private static void PopulatePools()
        {
            for (int i = 0; i < PoolSettings.Count; i++)
                if (!Pools.ContainsKey(PoolSettings[i].name))
                    Pools[PoolSettings[i].name] = new Pool(PoolSettings[i]);
        }

        public static GameObject Get(string poolName, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject obj = Get(poolName, position, rotation);
            if (obj) obj.transform.SetParent(parent);
            return obj;
        }

        public static GameObject Get(string poolName, Vector3 position, Quaternion rotation)
        {
            GameObject obj = Get(poolName, position);
            if (obj) obj.transform.rotation = rotation;
            return obj;
        }

        public static GameObject Get(string poolName, Vector3 position)
        {
            GameObject obj = Get(poolName);
            if (obj) obj.transform.position = position;
            return obj;
        }

        public static GameObject Get(string poolName)
        {
            if (PoolManager.HasPool(poolName))
            {    
                return Instance.GetPool(poolName).Get;
            }
            return null;
        }

        public static GameObject[] Get(string poolName, int total)
        {
            GameObject[] objects = new GameObject[total];
            for (int i = 0; i < total; i++)
            {
                objects[i] = Pools[poolName].Get;
            }
            return objects;
        }

        public static int TotalInPool(string poolName)
        {
            return Pools.ContainsKey(poolName) ? Pools[poolName].TotalInPool : 0;
        }

        public static int TotalActive(string poolName)
        {
            return Pools.ContainsKey(poolName) ? Pools[poolName].TotalActive : 0;
        }

        /// <summary>
        /// Display information about a PoolManager
        /// </summary>
        void OnGUI()
        {
            if (showAnalytics)
            {
                for (int i = 0; i < PoolSettings.Count; i++)
                {
                    if (PoolSettings[i].showAnalytics)
                    {
                        string name = PoolSettings[i].name;
                        GUI.Label(
                            new Rect(10, 10 + (i * 40), 400, 20),
                            name
                        );
                        GUI.Label(
                            new Rect(10, 30 + (i * 40), 400, 20),
                            "Available in Pool: " + TotalInPool(name) + " Active: " + TotalActive(name)
                        );
                    }
                }
            }
        }
    }
}
