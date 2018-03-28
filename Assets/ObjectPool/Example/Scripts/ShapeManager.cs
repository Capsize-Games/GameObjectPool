using UnityEngine;

namespace GameObjectPool
{
    class ShapeManager : MonoBehaviour
    {
        string cubePool = "CubePool";
        string spherePool = "SpherePool";

        void Update()
        {
            if (Input.GetMouseButton(0))
                SetPosition(PoolManager.Get(cubePool));
            if (Input.GetMouseButton(1))
                SetPosition(PoolManager.Get(spherePool));
        }

        void SetPosition(GameObject obj)
        {
            if (obj == null) return;
            obj.transform.position = new Vector3(
                Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                0
            );
        }

        void OnGUI()
        {
            GUI.Label(
                new Rect(10, 10, 400, 20),
                cubePool
            );
            GUI.Label(
                new Rect(10, 30, 400, 20),
                "Total: " + PoolManager.TotalActive(cubePool) + " Active: " + PoolManager.TotalActive(cubePool)
            );

            GUI.Label(
                new Rect(10, 50, 400, 20),
                spherePool
            );
            GUI.Label(
                new Rect(10, 70, 400, 20),
                "Total: " + PoolManager.TotalActive(spherePool) + " Active: " + PoolManager.TotalActive(spherePool)
            );
        }
    }
}
