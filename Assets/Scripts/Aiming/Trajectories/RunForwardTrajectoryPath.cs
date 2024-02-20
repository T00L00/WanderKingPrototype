using UnityEngine;

namespace WK.Aiming {
  public class RunForwardTrajectoryPath : AbstractTrajectoryPath {
    private Vector3 direction;
    private float speedStart;
    
    public override Vector3 CalculatePositionAtTime(float time) {
      float easeTime = Mathf.Lerp(0, duration, Easing.Quintic.Out(time/duration));

      return startPosition + direction * (easeTime / duration) * speedStart;
    }

    public override void CalculatePath(Vector3 startPosition, Vector3 endPosition) {
      Vector3 groundedBasePosition = new Vector3(startPosition.x, 0f, startPosition.z);
      Vector3 groundedTargetPosition = new Vector3(endPosition.x, 0f, endPosition.z);

      direction = (groundedTargetPosition - groundedBasePosition).normalized;
      duration = 2f;
      speedStart = Vector3.Distance(groundedBasePosition, groundedTargetPosition);
      this.startPosition = groundedBasePosition;
      destination = endPosition;
      launchAngle = 0f;
    }
    
    public override Vector3[] GetPath() {
      return new Vector3[]
      {
        startPosition,
        destination
      };
    }
  }
}