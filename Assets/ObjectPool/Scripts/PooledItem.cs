using UnityEngine;

namespace GameObjectPool
{
    public class PooledItem : MonoBehaviour
    {
        Pool pool;
        Rigidbody rb;
        Transform trans;

        public Pool Pool
        {
            get { return pool; }
            set { pool = value; }
        }

        public Rigidbody Rigidbody
        {
            get { return rb; }
        }

        void OnDisable()
        {
            Pool.Deactivate(gameObject);
        }

        void OnEnable()
        {
            if (Rigidbody == null) rb = GetComponent<Rigidbody>();
            if (Rigidbody != null)
            {
                Rigidbody.velocity = Vector3.zero;
                Rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}
