using UnityEngine;

namespace WK.Aiming
{
    public class ParabolicProjectileTrajectory : ProjectileTrajectory
    {
        private float gravity;
        private float step;
        private float speedStart;
        private Vector3 direction;
        private float maxHeight;
    
        public ParabolicProjectileTrajectory(float step, float gravity, float maxHeight = 0f)
        {
            this.step = step;
            this.gravity = gravity;
            this.maxHeight = maxHeight;
        }

        public override void CalculatePath(Vector3 start, Vector3 end)
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
      
            Duration = tplus > tminus ? tplus : tminus;
            LaunchAngle = Mathf.Atan(b * Duration / targetX);
            speedStart = b / Mathf.Sin(LaunchAngle);
            direction = groundDirection.normalized;
            StartPosition = start;
            Destination = end;
        }

        public override Vector3[] GetPath()
        {
            return CalculateParabolicPath(this.direction, speedStart, LaunchAngle, Duration);
        }
   
        public override Vector3 CalculatePositionAtTime(float time) 
        {
            float easeTime = Mathf.Lerp(0, Duration, Easing.Back.Out(time/Duration));
      
            float x = speedStart * Mathf.Cos(LaunchAngle) * easeTime;
            float y = speedStart * Mathf.Sin(LaunchAngle) * easeTime - 0.5f * gravity * Mathf.Pow(easeTime, 2);
            return StartPosition + direction*x + Vector3.up * y;
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
                path[count] = StartPosition + direction * x + Vector3.up * y;
                count++;
            }

            float xFinal = v0 * time * Mathf.Cos(angle);
            float yFinal = v0 * time * Mathf.Sin(angle) - 0.5f * gravity * Mathf.Pow(time, 2);
            path[count] = StartPosition + direction*xFinal + Vector3.up*yFinal;

            return path;
        }
    }
}