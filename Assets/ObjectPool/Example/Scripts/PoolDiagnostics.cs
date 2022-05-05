using ObjectPool.Scripts;
using UnityEngine;

namespace ObjectPool.Example.Scripts
{
    internal class PoolDiagnostics : BenchMarker
    {
        public string poolName = "";

        private void Update()
        {
            if (Input.GetMouseButton(0)) StartDiagnostics(5000);
            if (RunDiagnostics) Run();
        }

        private new void Run()
        {
            runStart = Time.realtimeSinceStartup;
            var obj = PoolManager.Get(poolName);
            base.Run();
            if (obj == null) return;
            if (Camera.main != null)
                obj.transform.position = new Vector3(
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                    0
                );
        }
    }
}