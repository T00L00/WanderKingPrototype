using UnityEngine;

namespace WK.Aiming {
  public abstract class AbstractAiming : MonoBehaviour
  {
    public AbstractTrajectoryPath TrajectoryPath { get; protected set; }

    public abstract void DrawPath(Vector3 startPosition, Vector3 endPosition);
    
    public abstract void Init();
    public abstract void Clear();
  }
}