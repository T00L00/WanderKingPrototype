using UnityEngine;

namespace WK.Aiming 
{
    [RequireComponent(typeof(LineRenderer))]
    public class ProjectileTrajectoryRenderer : MonoBehaviour
    {
        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void Init()
        {
            lineRenderer.enabled = true; ;
        }

        public void Clear()
        {
            lineRenderer.enabled = false;
            lineRenderer.positionCount = 0;
        }

        public void SetPath(Vector3[] path)
        {
            lineRenderer.positionCount = path.Length;
            for (var i = 0; i < path.Length; i++)
            {
                lineRenderer.SetPosition(i, path[i]);
            }
        }
    }
}