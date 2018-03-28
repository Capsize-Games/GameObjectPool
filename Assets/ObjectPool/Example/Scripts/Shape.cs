using UnityEngine;

namespace GameObjectPool
{
    class Shape : MonoBehaviour
    {
        public float timeOutSpeed = 0.2f;
        private float timeout = 0;

        void Awake()
        {
            timeout = 0;
        }

        void Update()
        {
            timeout += Time.deltaTime * timeOutSpeed;

            if (timeout >= 1f)
            {
                timeout = 0;
                transform.gameObject.SetActive(false);
            }
        }
    }
}
