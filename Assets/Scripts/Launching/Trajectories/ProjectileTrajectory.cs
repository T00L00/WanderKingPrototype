using UnityEngine;

namespace WK.Aiming 
{
    public abstract class ProjectileTrajectory
    {
        public float Duration { get; protected set; }
        public Vector3 Destination { get; protected set; }
        public Vector3 StartPosition { get; protected set; }
        public float LaunchAngle { get; protected set; }
    
        public abstract Vector3 CalculatePositionAtTime(float time);
        public abstract void CalculatePath(Vector3 startPosition, Vector3 endPosition);
        public abstract Vector3[] GetPath();
    }
}