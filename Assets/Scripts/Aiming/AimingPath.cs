using UnityEngine;

namespace WK.Aiming {
  public abstract class AimingPath : MonoBehaviour
  {
    public Vector3[] path { get; protected set; }
    public Vector3 Direction { get; protected set; }
    public float Angle { get; protected set; }
    public float SpeedStart { get; protected set; }
    public float Duration { get; protected set; }

    public abstract void DrawPath(Vector3 startPosition, Vector3 endPosition);
    public abstract void Init();
    public abstract void Clear();
  }
}