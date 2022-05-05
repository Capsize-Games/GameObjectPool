using UnityEngine;

namespace ObjectPool.Example.Scripts
{
    internal class BenchMarker : MonoBehaviour
    {
        #region Fields
        private int _maxRuns;
        private string _report;
        private float[] _results;
        private bool _runDiagnostics;
        protected float runStart;
        private float _total;
        private int _totalRuns;
        #endregion

        protected bool RunDiagnostics => _totalRuns < _maxRuns && _runDiagnostics;

        private void OnGUI()
        {
            if (_report != null)
                GUI.Label(
                    new Rect(10, 90, 600, 20),
                    _report
                );
        }

        protected void StartDiagnostics(int maxRuns)
        {
            _maxRuns = maxRuns;
            _runDiagnostics = true;
            _results = new float[maxRuns];
            _totalRuns = 0;
            _total = 0;
        }


        protected void Run()
        {
            var t = Time.realtimeSinceStartup - runStart;
            _results[_totalRuns] = t;
            _total += _results[_totalRuns];
            _report = "Average runtime (" + (_totalRuns + 1) + " iterations): " + _total / _totalRuns +
                     " with a total runtime of " + _total;
            _totalRuns += 1;
            if (_totalRuns >= _maxRuns) _runDiagnostics = false;
        }
    }
}