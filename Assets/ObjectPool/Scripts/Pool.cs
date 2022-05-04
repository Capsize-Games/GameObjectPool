using UnityEngine;
using System;
using System.Collections;
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
            get { return activeItems; }
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

        bool CanDequeue
        {
            get
            {
                //return TotalActive < TotalInPool;
                return TotalInPool > 0;
            }
        }

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
                g.SetActive(false);
                g.transform.SetParent(settings.parent);
                var pi = g.AddComponent<PooledItem>();
                pi.Pool = this;
                return g;
            }
        }
        #endregion

        public Pool(PoolSettings poolSettings) : base()
        {
            this.settings = poolSettings;
            InitializePool();
        }

        void InitializePool()
        {
            activeItems = new List<GameObject>();
            var max = settings.startingItemCount;
            if (!settings.allowUnrestrictedGrowth && max > settings.maxItemCount) max = settings.maxItemCount;
            for (int i = 0; i < max; i++)
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
