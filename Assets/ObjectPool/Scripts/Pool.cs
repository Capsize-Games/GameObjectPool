using UnityEngine;
using System.Collections.Generic;

namespace GameObjectPool
{
    public class Pool : Queue<GameObject>
    {
        PoolSettings settings;
        List<GameObject> activeItems;

        #region Properties
        List<GameObject> ActiveItems
        {
            get
            {
                if (activeItems == null) activeItems = new List<GameObject>();
                return activeItems;
            }
        }

        public PoolSettings Settings
        {
            get { return settings; }
        }

        public int TotalActive
        {
            get { return ActiveItems.Count; }
        }

        public int TotalInPool
        {
            get { return Count; }
        }

        bool GrowPool
        {
            get
            {
                return Count == 0 && (settings.allowUnrestrictedGrowth || TotalActive < settings.maxItemCount);
            }
        }

        bool CanDequeue => TotalInPool > 0;

        public GameObject Get
        {
            get
            {
                GameObject obj = null;
                if (CanDequeue) obj = Dequeue();
                if (obj == null) obj = (GrowPool) ? NewItem : null;
                if (obj != null)
                {
                    ActiveItems.Add(obj);
                    obj.SetActive(true);
                }
                return obj;
            }
        }

        private GameObject NewItem
        {
            get
            {
                var g = GameObject.Instantiate(settings.prefab);
                g.transform.SetParent(settings.parent);
                g.AddComponent<PooledItem>();
                InitializePoolItem(g);
                g.SetActive(false);
                return g;
            }
        }
        #endregion

        public Pool(PoolSettings poolSettings) : base()
        {
            settings = poolSettings;
            InitializePool();
        }

        private void InitializePoolItem(GameObject obj)
        {
            var pi = obj.GetComponent<PooledItem>();
            if (pi == null) return;
            pi.name = settings.name;
            pi.Pool = this;
        }

        private bool GameObjectBelongsToThisPool(GameObject obj)
        {
            var pooledItem = obj.GetComponent<PooledItem>();
            return pooledItem != null && pooledItem.name == settings.name;
        }

        private void FindExistingPoolItems()
        {
            GameObject[] objects = Object.FindObjectsOfType<GameObject>(true);
            foreach (var obj in objects)
            {
                if (GameObjectBelongsToThisPool(obj))
                {
                    InitializePoolItem(obj);
                    Insert(obj);
                }
            }
        }

        public void InitializePool()
        {
            FindExistingPoolItems();
            var max = settings.startingItemCount;
            if (!settings.allowUnrestrictedGrowth && max > settings.maxItemCount) max = settings.maxItemCount;
            for (int i = TotalInPool; i < max; i++)
                Insert(NewItem);
        }

        private void Insert(GameObject obj)
        {
            Enqueue(obj);
        }

        public void DeactivateAll()
        {
            foreach (GameObject obj in ActiveItems) obj.SetActive(false);
        }

        public void Deactivate(GameObject obj)
        {
            ActiveItems.Remove(obj);
            Enqueue(obj);
        }

        public void DestroyAll()
        {
            foreach (var obj in ActiveItems) Object.Destroy(obj);
            foreach (var obj in this) Object.Destroy(obj);
        }
    }
}
