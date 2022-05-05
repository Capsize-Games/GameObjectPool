using ObjectPool.Scripts;
using UnityEngine;

namespace ObjectPool.Example.Scripts
{
    class PoolDiagnostics : Benchmarker
    {
        public string poolName = "";

        void Update()
        {
            if (Input.GetMouseButton(0)) StartDiagnostics(5000);
            if (RunDiagnostics) Run();
        }

        new protected void Run()
        {
            runstart = Time.realtimeSinceStartup;
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
