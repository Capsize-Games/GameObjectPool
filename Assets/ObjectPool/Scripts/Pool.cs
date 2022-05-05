using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool.Scripts
{
    public class Pool : Queue<GameObject>
    {
        private List<GameObject> activeItems;

        public Pool(PoolSettings poolSettings)
        {
            Settings = poolSettings;
            InitializePool();
        }

        private void InitializePoolItem(GameObject obj)
        {
            var pi = obj.GetComponent<PooledItem>();
            if (pi == null) return;
            pi.name = Settings.name;
            pi.Pool = this;
        }

        private bool GameObjectBelongsToThisPool(GameObject obj)
        {
            var pooledItem = obj.GetComponent<PooledItem>();
            return pooledItem != null && pooledItem.name == Settings.name;
        }

        private void FindExistingPoolItems()
        {
            var objects = Object.FindObjectsOfType<GameObject>(true);
            foreach (var obj in objects)
                if (GameObjectBelongsToThisPool(obj))
                {
                    InitializePoolItem(obj);
                    Insert(obj);
                }
        }

        public void InitializePool()
        {
            FindExistingPoolItems();
            var max = Settings.startingItemCount;
            if (!Settings.allowUnrestrictedGrowth && max > Settings.maxItemCount) max = Settings.maxItemCount;
            for (var i = TotalInPool; i < max; i++)
                Insert(NewItem);
        }

        private void Insert(GameObject obj)
        {
            Enqueue(obj);
        }

        public void DeactivateAll()
        {
            foreach (var obj in ActiveItems) obj.SetActive(false);
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

        #region Properties

        private List<GameObject> ActiveItems
        {
            get
            {
                if (activeItems == null) activeItems = new List<GameObject>();
                return activeItems;
            }
        }

        public PoolSettings Settings { get; }

        public int TotalActive => ActiveItems.Count;

        public int TotalInPool => Count;

        private bool GrowPool =>
            Count == 0 && (Settings.allowUnrestrictedGrowth || TotalActive < Settings.maxItemCount);

        private bool CanDequeue => TotalInPool > 0;

        public GameObject Get
        {
            get
            {
                GameObject obj = null;
                if (CanDequeue) obj = Dequeue();
                if (obj == null) obj = GrowPool ? NewItem : null;
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
                var g = Object.Instantiate(Settings.prefab);
                g.transform.SetParent(Settings.parent);
                g.AddComponent<PooledItem>();
                InitializePoolItem(g);
                g.SetActive(false);
                return g;
            }
        }

        #endregion
    }
}