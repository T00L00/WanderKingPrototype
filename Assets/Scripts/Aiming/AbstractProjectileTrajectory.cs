using UnityEngine;

namespace WK.Aiming {
  public abstract class AbstractProjectileTrajectory
  {
    public float duration { get; protected set; }
    public abstract Vector3 CalculatePositionAtTime(float time);
    public abstract Vector3[] GetPath(Vector3 startPosition, Vector3 endPosition);
  }
}