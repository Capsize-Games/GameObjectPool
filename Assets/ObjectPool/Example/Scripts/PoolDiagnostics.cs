using UnityEngine;

namespace GameObjectPool
{
    class PoolDiagnostics : Benchmarker
    {
        public string poolName = "";

        protected override void Run()
        {
            var obj = PoolManager.Get(poolName);
            base.Run();
            if (obj == null) return;
            obj.transform.position = new Vector3(
                Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                0
            );
        }
    }
}
