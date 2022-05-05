using ObjectPool.Scripts;
using UnityEngine;

namespace ObjectPool.Example.Scripts
{
    /// <summary>
    ///     This class is meant to demonstrate how to use an object pool.
    ///     Use the same techniques found in this class wherever you need to spawn a game object from the Object Pool.
    /// </summary>
    internal class ShapeManager : MonoBehaviour
    {
        private const string CubePool = "CubePool";
        private const string SpherePool = "SpherePool";
        public GameObject spherePrefab;

        // Use the following code to add a pool at run-time (not recommended)
        /*
        private void Awake()
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
        */

        /// <summary>
        ///     Listen for mouse clicks and get an object from the appropriate PoolManager.
        ///     After an object is returned from the PoolManager, it is passed to the SetPosition method.
        ///     These same techniques could be used for things such as FPS bullet spawners in an FPS game.
        /// </summary>
        private void Update()
        {
            var location = new Vector3(
                Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                0
            );

            // On left mouse click, spawn a cube object from the cube pool.
            if (Input.GetMouseButton(0)) PoolManager.Get(CubePool, location, Random.rotation);

            // On right mouse click, spawn a sphere object from the sphere pool.
            if (Input.GetMouseButton(1)) PoolManager.Get(SpherePool, location, Random.rotation);
        }
    }
}