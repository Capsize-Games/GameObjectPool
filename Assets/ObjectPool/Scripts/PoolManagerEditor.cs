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
            PoolManager myScript = (PoolManager)target;
            if (GUILayout.Button("Add Pool"))
            {
                myScript.m_pool.Add(new PoolSettings());
            }
        }
    }
}