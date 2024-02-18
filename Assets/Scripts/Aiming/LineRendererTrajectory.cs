using System;
using UnityEngine;

namespace WK.Aiming {
  
  [RequireComponent(typeof(LineRenderer))]
  public class LineRendererTrajectory : AbstractTrajectoryRender
  {
    private LineRenderer lineRenderer;

    private void Awake()
    {
      lineRenderer = GetComponent<LineRenderer>();
    }

    public override void Init()
    {
      gameObject.SetActive(true);
    }
    
    public override void Clear()
    {
      gameObject.SetActive(false);
      lineRenderer.positionCount = 0;
    }
    
    public override void SetPath(Vector3[] path)
    {
      lineRenderer.positionCount = path.Length;
      for (var i = 0; i < path.Length; i++)
      {
        lineRenderer.SetPosition(i, path[i]);
      }
    }
  }
}