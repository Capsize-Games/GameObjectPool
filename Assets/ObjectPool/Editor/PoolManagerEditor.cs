using UnityEngine;
using ObjectPool.Scripts;
using UnityEditor;

namespace GameObjectPool
{
    [CustomEditor(typeof(PoolManager))]
    public class PoolManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var poolManager = (PoolManager)target;

            if (GUILayout.Button("Add Pool"))
            {
                poolManager.mPool.Add(new PoolSettings());
            }

            if (GUILayout.Button("Regenerate pools"))
            {
                poolManager.GeneratePools();
            }
        }
    }
}