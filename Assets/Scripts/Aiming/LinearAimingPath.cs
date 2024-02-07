using UnityEngine;

namespace WK.Aiming
{
  public class LinearAimingPath : AimingPath
  {
    
    [SerializeField] private LineRenderer lineRenderer;
    
    public override void DrawPath(Vector3 startPosition, Vector3 endPosition)
    {
      Vector3 groundedBasePosition = new Vector3(startPosition.x, 0, startPosition.z);
      Vector3 groundedTargetPosition = new Vector3(endPosition.x, 0f, endPosition.z);

      path = new Vector3[]
      {
        groundedBasePosition,
        groundedTargetPosition
      };

      DrawLinearPath();
    }

    public override void Init()
    {
      gameObject.SetActive(true);
    }

    public override void Clear()
    {
      gameObject.SetActive(false);
      path = new Vector3[]{};
      lineRenderer.positionCount = 0;
    }
    
    private void DrawLinearPath()
    {
      lineRenderer.positionCount = path.Length;
      for (var i = 0; i < path.Length; i++)
      {
        lineRenderer.SetPosition(i, path[i]);
      }
    }
  }
}