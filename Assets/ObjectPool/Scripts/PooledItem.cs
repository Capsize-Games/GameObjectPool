using UnityEngine;

namespace GameObjectPool
{
    public class PooledItem : MonoBehaviour
    {
        public string name;

        public Pool Pool;
        
        public Rigidbody Rigidbody { get; private set; }

        void OnDisable()
        {
            Pool.Deactivate(gameObject);
        }

        void OnEnable()
        {
            if (Rigidbody == null) Rigidbody = GetComponent<Rigidbody>();
            if (Rigidbody != null)
            {
                Rigidbody.velocity = Vector3.zero;
                Rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}
