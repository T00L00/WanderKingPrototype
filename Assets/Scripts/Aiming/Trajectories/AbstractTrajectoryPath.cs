using UnityEngine;

namespace WK.Aiming {
  public abstract class AbstractTrajectoryPath
  {
    public float duration { get; protected set; }
    public Vector3 destination { get; protected set; }
    public Vector3 startPosition { get; protected set; }
    
    public abstract Vector3 CalculatePositionAtTime(float time);
    public abstract Vector3[] GetPath(Vector3 startPosition, Vector3 endPosition);
  }
}