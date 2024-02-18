using UnityEngine;

namespace WK.Aiming
{
  public class ChargeAbstractAiming : AbstractAiming
  {
    
    [SerializeField] private TrajectoryRender trajectoryRender;

    private Vector3 startPosition;

    public override void DrawPath(Vector3 startPosition, Vector3 endPosition) {
      this.startPosition = startPosition;
      
      Vector3 groundedBasePosition = new Vector3(startPosition.x, 0, startPosition.z);
      Vector3 groundedTargetPosition = new Vector3(endPosition.x, 0f, endPosition.z);

      Direction = (groundedTargetPosition - groundedBasePosition).normalized;
      Angle = 0f;
      Duration = 2f;
      SpeedStart = Vector3.Distance(groundedBasePosition, groundedTargetPosition);
      
      path = new Vector3[]
      {
        groundedBasePosition,
        groundedTargetPosition
      };
      
      trajectoryRender.SetPath(path);
    }

    public override void Init()
    {
      gameObject.SetActive(true);
      trajectoryRender.Init();
    }

    public override void Clear()
    {
      gameObject.SetActive(false);
      path = new Vector3[]{};
      trajectoryRender.Clear();
    }

    public override Vector3 CalculatePositionFromTime(float time)
    {
      return startPosition + Direction * (time / Duration) * SpeedStart;
    }

  }
}