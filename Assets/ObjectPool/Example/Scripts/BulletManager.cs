using UnityEngine;

namespace GameObjectPool
{
    class BulletManager : MonoBehaviour
    {
        public float timeOutSpeed = 0.2f;
        private float timeout = 0;

        void Update()
        {
            timeout += Time.deltaTime * timeOutSpeed;

            if (timeout >= 1f) SpawnBullet();
        }

        void SpawnBullet()
        {
            PoolManager.Get("PlayerBulletPool");
            PoolManager.Get("EnemyBulletPool");
            timeout = 0;
        }
    }
}
