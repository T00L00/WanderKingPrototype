using UnityEngine;

namespace WK.Aiming {
  public class ParabolicAimingPath : AimingPath {
    
    private float v0;
    private float angle;
    private float time;
    private float gravity;
    private float step;
    private Vector3 basePosition;


    public void Init(float step, float gravity) {
      this.step = Mathf.Max(0.01f, step);
      this.gravity = gravity;
    }
    
    public override void Calculate(Vector3 basePosition, Vector3 targetPosition) {
      Vector3 direction = targetPosition - basePosition;
      Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
      Vector3 correctedTargetPosition = new Vector3(groundDirection.magnitude, direction.y, 0f);
      float height = correctedTargetPosition.y + correctedTargetPosition.magnitude / 2f;
      height = Mathf.Max(0.1f, height);
      
      this.basePosition = basePosition;

      float targetX = correctedTargetPosition.x;
      float targetY = correctedTargetPosition.y;
        
      float b = Mathf.Sqrt(2 * gravity * height);
      float a = (-0.5f * gravity);
      float c = -targetY;

      float tplus = QuadraticEquation(a, b, c, true);
      float tminus = QuadraticEquation(a, b, c, false);
      time = tplus > tminus ? tplus : tminus;
        
      angle = Mathf.Atan(b * time / targetX);
      v0 = b / Mathf.Sin(angle);

      DrawParabolicPath(groundDirection.normalized);
    }
    
    private void DrawParabolicPath(Vector3 direction) {
      path = new Vector3[(int)(time / step) + 2];
      

      int count = 0;
      for (float t = 0; t < time; t += step) {
        float x = v0 * Mathf.Cos(angle) * t;
        float y = v0 * Mathf.Sin(angle) * t - 0.5f * gravity * Mathf.Pow(t, 2);
        path[count] = basePosition + direction * x + Vector3.up * y;
        count++;
      }

      float xFinal = v0 * time * Mathf.Cos(angle);
      float yFinal = v0 * time * Mathf.Sin(angle) - 0.5f * gravity * Mathf.Pow(time, 2);
      path[count] = basePosition + direction*xFinal + Vector3.up*yFinal;
    }
    
    private float QuadraticEquation(float a, float b, float c, bool positive) {
      float sign = positive ? 1 : -1;
      float determinant = Mathf.Pow(b, 2) - 4 * a * c;
      if (determinant < 0) return float.NaN;
      float sqrt = sign * Mathf.Sqrt(determinant);
      float result = (-b + sqrt) / (2 * a);
      return result;
    }

    
  }
}