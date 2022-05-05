using UnityEngine;

namespace ObjectPool.Scripts
{
    public class PooledItem : MonoBehaviour
    {
        public new string name;
        public Pool pool;
        private Rigidbody Rigidbody { get; set; }

        private void OnEnable()
        {
            if (Rigidbody == null) Rigidbody = GetComponent<Rigidbody>();
            if (Rigidbody != null)
            {
                Rigidbody.velocity = Vector3.zero;
                Rigidbody.angularVelocity = Vector3.zero;
            }
        }

        private void OnDisable()
        {
            if (gameObject == null) return;
            pool?.Deactivate(gameObject);
        }
    }
}