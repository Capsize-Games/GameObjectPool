using UnityEngine;

namespace ObjectPool.Example.Scripts
{
    internal class Shape : MonoBehaviour
    {
        public float timeOutSpeed = 0.2f;
        private float _timeout;

        private void Awake()
        {
            _timeout = 0;
        }

        private void Update()
        {
            _timeout += Time.deltaTime * timeOutSpeed;

            if (_timeout >= 1f)
            {
                _timeout = 0;
                transform.gameObject.SetActive(false);
            }
        }
    }
}