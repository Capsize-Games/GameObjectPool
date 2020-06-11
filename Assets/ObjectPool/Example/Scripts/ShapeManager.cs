using UnityEngine;

namespace GameObjectPool
{
    /// <summary>
    /// This class is meant to demonstrate how to use an object pool.
    /// Use the same techniques found in this class wherever you need to spawn a game object from the Object Pool.
    /// </summary>
    class ShapeManager : MonoBehaviour
    {
        const string cubePool = "CubePool";
        const string spherePool = "SpherePool";
        public GameObject spherePrefab;

        private void Start()
        {
            PoolManager.AddPool(new PoolSettings
            {
                name = "SpherePool",
                prefab = spherePrefab,
                startingItemCount = 10,
                maxItemCount = 50,
                parent = this.transform,
                allowUnrestrictedGrowth = true
            });
        }

        /// <summary>
        /// Listen for mouse clicks and get an object from the appropriate PoolManager.
        /// After an object is returned from the PoolManager, it is passed to the SetPosition method.
        /// These same techniques could be used for things such as FPS bullet spawners in an FPS game.
        /// </summary>
        void Update()
        {
            Vector3 location = new Vector3(
                Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                0
            );

            // On left mouse click, spawn a cube object from the cube pool.
            if (Input.GetMouseButton(0)) PoolManager.Get(cubePool, location, Random.rotation);

            // On right mouse click, spawn a sphere object from the sphere pool.
            if (Input.GetMouseButton(1))
            {
                if (PoolManager.HasPool(spherePool))
                {
                    PoolManager.Get(spherePool, location, Random.rotation);
                }
            }
        }
    }
}
