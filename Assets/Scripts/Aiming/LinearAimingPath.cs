using UnityEngine;

namespace WK.Aiming {
  public class LinearAimingPath : AimingPath {
    
    private float step;
    
    public void Init(float step, float gravity) {
      this.step = Mathf.Max(0.01f, step);
    }
    
    public override void Calculate(Vector3 basePosition, Vector3 targetPosition) {
      Vector3 direction = targetPosition - basePosition;
      Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
      Vector3 correctedTargetPosition = new Vector3(groundDirection.magnitude, direction.y, 0f);
      path = new Vector3[] {
        basePosition,
        correctedTargetPosition
      };
    }
  }
}