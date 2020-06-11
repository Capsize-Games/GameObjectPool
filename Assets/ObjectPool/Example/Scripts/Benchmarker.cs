using UnityEngine;

namespace GameObjectPool
{
    class Benchmarker : MonoBehaviour
    {
        bool runDiagnostics;
        float[] results;
        int totalRuns;
        int maxRuns;
        string report;
        float total;
        protected float runstart;

        protected bool RunDiagnostics
        {
            get { return totalRuns < maxRuns && runDiagnostics; }
        }

        protected void StartDiagnostics(int maxRuns)
        {
            this.maxRuns = maxRuns;
            runDiagnostics = true;
            results = new float[maxRuns];
            totalRuns = 0;
            total = 0;
        }


        protected void Run()
        {
            float t = Time.realtimeSinceStartup - runstart;
            results[totalRuns] = t;
            total += results[totalRuns];
            report = "Average runtime (" + (totalRuns + 1) + " iterations): " + total / (float)totalRuns + " with a total runtime of " + total;
            totalRuns += 1;
            if (totalRuns >= maxRuns) runDiagnostics = false;
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
