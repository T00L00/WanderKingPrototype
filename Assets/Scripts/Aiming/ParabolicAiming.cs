using UnityEngine;

namespace WK.Aiming
{
  public class ParabolicAiming : AbstractAiming
  {
    
    [SerializeField] private float step;
    [SerializeField] private float maxHeight;
    [SerializeField] private bool hasMaxHeight;
    [SerializeField] private float gravity;
    [SerializeField] private AbstractTrajectoryRender abstractTrajectoryRender;
    
    private Vector3 startPosition;
    
    public override void DrawPath(Vector3 startPosition, Vector3 endPosition)
    {
      this.startPosition = startPosition;
      
      Vector3 direction = endPosition - startPosition;
      Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
      Vector3 correctedTargetPosition = new Vector3(groundDirection.magnitude, direction.y, 0f);
      
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
      Duration = tplus > tminus ? tplus : tminus;
        
      Angle = Mathf.Atan(b * Duration / targetX);
      SpeedStart = b / Mathf.Sin(Angle);
      Direction = groundDirection.normalized;
      
      CalculateParabolicPath(Direction, SpeedStart, Angle, Duration);
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

    private void CalculateParabolicPath(Vector3 direction, float v0, float angle, float time)
    {
      path = new Vector3[(int)(time / step) + 2];

      int count = 0;
      for (float t = 0; t < time; t += step) {
        float x = v0 * Mathf.Cos(angle) * t;
        float y = v0 * Mathf.Sin(angle) * t - 0.5f * gravity * Mathf.Pow(t, 2);
        path[count] = startPosition + direction * x + Vector3.up * y;
        count++;
      }

      float xFinal = v0 * time * Mathf.Cos(angle);
      float yFinal = v0 * time * Mathf.Sin(angle) - 0.5f * gravity * Mathf.Pow(time, 2);
      path[count] = startPosition + direction*xFinal + Vector3.up*yFinal;
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

    public override Vector3 CalculatePositionFromTime(float time)
    {
      float x = SpeedStart * Mathf.Cos(Angle) * time;
      float y = SpeedStart * Mathf.Sin(Angle) * time - 0.5f * gravity * Mathf.Pow(time, 2);
      return startPosition + Direction*x + Vector3.up*y;
    }
  }
}