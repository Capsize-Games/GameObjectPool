using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameObjectPool
{
	public class Pool : Queue
	{
		private PoolSettings settings;
		private int totalInPool = 0;

        private List<GameObject> activeItems;
        public List<GameObject> ActiveItems
        {
            get
            {
                if (activeItems == null) activeItems = new List<GameObject>();
                return activeItems;
            }
        }

		public Pool(PoolSettings poolSettings) : base()
		{
            this.settings = poolSettings;

			for (int i = 0; i < settings.maxItemCount; i++) Insert();
		}

		public GameObject Get()
		{
            try
            {
                GameObject obj = (GameObject) Dequeue();
                ActiveItems.Add(obj);
				return obj;
            }
            catch(InvalidOperationException e)
            {
				totalInPool += 1;
				GameObject obj = InsertAndGet();

				if (!settings.allowUnrestrictedGrowth) Debug.LogError(e.Message + " " +settings.name + " pool is full at "+ totalInPool + "- leaking over by " + (
					totalInPool - settings.maxItemCount
				));

				return obj;
            }
		}

        private GameObject InsertAndGet()
		{
            Insert();
            return Get();
		}

        private void Insert()
		{
            GameObject obj = (GameObject) GameObject.Instantiate(settings.prefab);
			obj.AddComponent<PooledItem>();
			PooledItem pi = obj.GetComponent<PooledItem>();
			pi.poolName = settings.name;
			obj.transform.SetParent(settings.parent);
			obj.SetActive(false);
			totalInPool += 1;
			Enqueue(obj);
		}

        public void DeactivateAll()
		{
			foreach (GameObject obj in ActiveItems) Deactivate(obj);
		}

        public void Deactivate(GameObject obj)
        {
            ActiveItems.Remove(obj);
            obj.SetActive(false);
            Enqueue(obj);
        }

		public void DestroyAll()
		{
			foreach (GameObject obj in ActiveItems) GameObject.Destroy(obj);
            foreach (GameObject obj in this) GameObject.Destroy(obj);
			totalInPool = 0;
		}
	}
}
