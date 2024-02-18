using UnityEngine;

namespace WK.Aiming
{
  public class ChargeAiming : AbstractAiming
  {
    
    [SerializeField] private AbstractTrajectoryRender abstractTrajectoryRender;

    private Vector3 startPosition;

    public override void DrawPath(Vector3 startPosition, Vector3 endPosition)
    {
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
      
      abstractTrajectoryRender.SetPath(path);
    }

    public override void Init()
    {
      gameObject.SetActive(true);
      abstractTrajectoryRender.Init();
    }

    public override void Clear()
    {
      gameObject.SetActive(false);
      path = new Vector3[]{};
      abstractTrajectoryRender.Clear();
    }

    public override Vector3 CalculatePositionFromTime(float time)
    {
      return startPosition + Direction * (time / Duration) * SpeedStart;
    }

  }
}