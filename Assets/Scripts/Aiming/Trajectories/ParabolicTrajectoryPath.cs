using UnityEngine;

namespace WK.Aiming
{
  public class ParabolicTrajectoryPath : AbstractTrajectoryPath
  {
    private float gravity;
    private float step;
    private float angle;
    private float speedStart;
    private Vector3 direction;
    private float maxHeight;
    
    public ParabolicTrajectoryPath(float step, float gravity, float maxHeight = 0f)
    {
      this.step = step;
      this.gravity = gravity;
      this.maxHeight = maxHeight;
    }

    public override Vector3[] GetPath(Vector3 start, Vector3 end)
    {
      bool hasMaxHeight = maxHeight >= 0;
      Vector3 directionPosition = (end - start);
      Vector3 groundDirection = new Vector3(directionPosition.x, 0, directionPosition.z);
      Vector3 correctedTargetPosition = new Vector3(groundDirection.magnitude, directionPosition.y, 0f);
      
      float newHeight = correctedTargetPosition.y + correctedTargetPosition.magnitude / 2f;
      newHeight = hasMaxHeight ? Mathf.Min(newHeight, maxHeight) : newHeight;
      newHeight = Mathf.Max(0.1f, newHeight);

      float targetX = correctedTargetPosition.x;
      float targetY = correctedTargetPosition.y;
        
      float b = Mathf.Sqrt(2 * gravity * newHeight);
      float a = (-0.5f * gravity);
      float c = -targetY;

      float tplus = QuadraticEquation(a, b, c, true);
      float tminus = QuadraticEquation(a, b, c, false);
      
      duration = tplus > tminus ? tplus : tminus;
      angle = Mathf.Atan(b * duration / targetX);
      speedStart = b / Mathf.Sin(angle);
      direction = groundDirection.normalized;
      startPosition = start;
      destination = end;
      
      return CalculateParabolicPath(this.direction, speedStart, angle, duration);
    }
    
    public override Vector3 CalculatePositionAtTime(float time)
    {
      float x = speedStart * Mathf.Cos(angle) * time;
      float y = speedStart * Mathf.Sin(angle) * time - 0.5f * gravity * Mathf.Pow(time, 2);
      return startPosition + direction*x + Vector3.up * y;
    }
    
    private float QuadraticEquation(float a, float b, float c, bool positive)
    {
      float sign = positive ? 1 : -1;
      float determinant = Mathf.Pow(b, 2) - 4 * a * c;
      if (determinant < 0) return float.NaN;
      float sqrt = sign * Mathf.Sqrt(determinant);
      float result = (-b + sqrt) / (2 * a);
      return result;
    }
    
    private  Vector3[] CalculateParabolicPath(Vector3 direction, float v0, float angle, float time)
    {
      Vector3[] path = new Vector3[(int)(time / step) + 2];

      int count = 0;
      for (float t = 0; t < time; t += step)
      {
        float x = v0 * Mathf.Cos(angle) * t;
        float y = v0 * Mathf.Sin(angle) * t - 0.5f * gravity * Mathf.Pow(t, 2);
        path[count] = startPosition + direction * x + Vector3.up * y;
        count++;
      }

      float xFinal = v0 * time * Mathf.Cos(angle);
      float yFinal = v0 * time * Mathf.Sin(angle) - 0.5f * gravity * Mathf.Pow(time, 2);
      path[count] = startPosition + direction*xFinal + Vector3.up*yFinal;

      return path;
    }
  }
}