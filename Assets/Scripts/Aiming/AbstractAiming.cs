using UnityEngine;

namespace WK.Aiming {
  public abstract class AbstractAiming : MonoBehaviour
  {
    public ProjectileTrajectory TrajectoryPath { get; protected set; }

    public abstract void DrawPath();
    public abstract void CalculatePath(Vector3 startPosition, Vector3 endPosition);
    
    public abstract void Init();
    public abstract void Clear();
  }
}