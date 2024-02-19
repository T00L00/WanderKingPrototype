using UnityEngine;

namespace WK.Aiming {
  public class RunForwardTrajectoryPath : AbstractTrajectoryPath {
    private Vector3 direction;
    private float speedStart;
    private Vector3 startPosition;
    
    public override Vector3 CalculatePositionAtTime(float time) {
      return startPosition + direction * (time / duration) * speedStart;
    }

    public override Vector3[] GetPath(Vector3 startPosition, Vector3 endPosition) {
      Vector3 groundedBasePosition = new Vector3(startPosition.x, 0f, startPosition.z);
      Vector3 groundedTargetPosition = new Vector3(endPosition.x, 0f, endPosition.z);

      direction = (groundedTargetPosition - groundedBasePosition).normalized;
      duration = 2f;
      speedStart = Vector3.Distance(groundedBasePosition, groundedTargetPosition);
      this.startPosition = startPosition;
      destination = endPosition;
      
      return new Vector3[]
      {
        groundedBasePosition,
        groundedTargetPosition
      };
    }
  }
}