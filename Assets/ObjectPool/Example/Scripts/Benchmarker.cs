using UnityEngine;

namespace ObjectPool.Example.Scripts
{
    internal class Benchmarker : MonoBehaviour
    {
        private int maxRuns;
        private string report;
        private float[] results;
        private bool runDiagnostics;
        protected float runstart;
        private float total;
        private int totalRuns;

        protected bool RunDiagnostics => totalRuns < maxRuns && runDiagnostics;

        private void OnGUI()
        {
            if (report != null)
                GUI.Label(
                    new Rect(10, 90, 600, 20),
                    report
                );
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
            var t = Time.realtimeSinceStartup - runstart;
            results[totalRuns] = t;
            total += results[totalRuns];
            report = "Average runtime (" + (totalRuns + 1) + " iterations): " + total / totalRuns +
                     " with a total runtime of " + total;
            totalRuns += 1;
            if (totalRuns >= maxRuns) runDiagnostics = false;
        }
    }
}