using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameObjectPool
{
    public class PoolManager : MonoBehaviour
    {
        public PoolSettings[] m_pool;

        void Start()
        {}

            private static void PopulatePools()
            {
                for (int i = 0; i < Instance.m_pool.Length; i++)
                {
                    if (!Pools.ContainsKey(Instance.m_pool[i].name))
                    {
                        Pools.Add(Instance.m_pool[i].name, null);
                        Pools[Instance.m_pool[i].name] = new Pool(Instance.m_pool[i]);
                    }
                }
            }

            private static PoolManager instance;
            private static PoolManager Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType(typeof (PoolManager)) as PoolManager;
                    }

                    if (!instance) Debug.LogError("PoolManager script required.");

                    return instance;
                }
            }

            private Dictionary<string, Pool> pools;
            private static Dictionary<string, Pool> Pools
            {
                get
                {
                    if (Instance.pools == null)
                    {
                        Instance.pools = new Dictionary<string, Pool>();
                    }
                    return Instance.pools;
                }
            }

            private Dictionary<string, List<GameObject>> activeItems;
            private static Dictionary<string, List<GameObject>> ActiveItems
            {
                get
                {
                    if (Instance.activeItems == null)
                    {
                        Instance.activeItems = new Dictionary<string, List<GameObject>>();
                        foreach (KeyValuePair<string, Pool> val in Pools)
                        {
                            Instance.activeItems.Add(val.Key, new List<GameObject>());
                        }
                    }
                    return Instance.activeItems;
                }
            }

            public static void Activate(string poolName, GameObject obj)
            {
                ActiveItems[poolName].Add(obj);
            }

            public static void Deactivate(string poolName, GameObject obj)
            {
                Dictionary<string, Pool> p = Pools;

                if (!ActiveItems.ContainsKey(poolName))
                {
                    ActiveItems.Add(poolName, new List<GameObject>());
                }
                else
                {
                    ActiveItems[poolName].Remove(obj);
                    if (Pools.ContainsKey(poolName) && Pools[poolName] != null) Pools[poolName].Enqueue(obj);
                }
            }

            public static void DeactivateAllActiveItems()
            {
                foreach (KeyValuePair<string, List<GameObject>> val in ActiveItems)
                {
                    foreach (GameObject item in ActiveItems[val.Key])
                    {
                        item.SetActive(false);
                    }
                }
            }

            public static GameObject Get(string poolName)
            {
                if (!Pools.ContainsKey(poolName)) PopulatePools();
                GameObject obj = Pools[poolName].Get();
                obj.SetActive(true);
                return obj;
            }
        }
    }
