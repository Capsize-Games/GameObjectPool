using UnityEngine;
using System.Collections;
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
                poolManager.m_pool.Add(new PoolSettings());
            }

            if (GUILayout.Button("Regenerate pools"))
            {
                poolManager.GeneratePools();
            }
        }
    }
}