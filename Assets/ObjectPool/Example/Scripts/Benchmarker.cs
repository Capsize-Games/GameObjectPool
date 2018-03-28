using UnityEngine;

namespace GameObjectPool
{
    class Benchmarker : MonoBehaviour
    {
        bool runDiagnostics;
        float[] results;
        float runTime;
        int totalRuns;
        int maxRuns;
        string report;
        float total;

        bool RunDiagnostics
        {
            get { return totalRuns < maxRuns && runDiagnostics; }
        }

        void StartDiagnostics(int maxRuns)
        {
            this.maxRuns = maxRuns;
            runDiagnostics = true;
            results = new float[maxRuns];
            runTime = 0;
            totalRuns = 0;
            total = 0;
        }

        protected virtual void Run()
        {
            float t = Time.deltaTime;
            runTime += t;
            results[totalRuns] = t;
            total += results[totalRuns];
            report = "Average runtime ("+ (totalRuns + 1) +" iterations): " + total / (float) totalRuns + " with a total runtime of " + runTime;
            totalRuns += 1;
            if (totalRuns >= maxRuns) runDiagnostics = false;
        }

        void Update()
        {
            if (Input.GetMouseButton(0)) StartDiagnostics(5000);
            if (RunDiagnostics) Run();
        }

        void OnGUI()
        {
            if (report != null)
                GUI.Label(
                    new Rect(10, 90, 600, 20),
                    report
                );
        }
    }
}
