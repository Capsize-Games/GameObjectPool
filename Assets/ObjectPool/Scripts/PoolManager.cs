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
            public PoolSettings[] m_pool;
            private static PoolManager instance;
            private Dictionary<string, Pool> pools;
        #endregion

        #region Properties
            public static PoolSettings[] PoolSettings
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
            PopulatePools();
        }

        private static void PopulatePools()
        {
            for (int i = 0; i < PoolSettings.Length; i++)
                if (!Pools.ContainsKey(PoolSettings[i].name))
                    Pools[PoolSettings[i].name] = new Pool(PoolSettings[i]);
        }

        public static GameObject Get(string poolName)
        {
            return Pools[poolName].Get;
        }

        public static int TotalInPool(string poolName)
        {
            return Pools.ContainsKey(poolName) ? Pools[poolName].TotalInPool : 0;
        }

        public static int TotalActive(string poolName)
        {
            return Pools.ContainsKey(poolName) ? Pools[poolName].TotalActive : 0;
        }
    }
}
