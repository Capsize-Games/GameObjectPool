using UnityEngine;

namespace ObjectPool.Example.Scripts
{
    internal class Shape : MonoBehaviour
    {
        public float timeOutSpeed = 0.2f;
        private float timeout;

        private void Awake()
        {
            timeout = 0;
        }

        private void Update()
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