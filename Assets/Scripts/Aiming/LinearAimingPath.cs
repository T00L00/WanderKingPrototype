using UnityEngine;

namespace WK.Aiming
{
  public class LinearAimingPath : AimingPath
  {
    public override void Calculate(Vector3 startPosition, Vector3 endPosition)
    {
      Vector3 groundedBasePosition = new Vector3(startPosition.x, 0, startPosition.z);
      Vector3 groundedTargetPosition = new Vector3(endPosition.x, 0f, endPosition.z);

      path = new Vector3[]
      {
        groundedBasePosition,
        groundedTargetPosition
      };
    }
  }
}