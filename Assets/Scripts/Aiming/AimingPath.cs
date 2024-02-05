using UnityEngine;

namespace WK.Aiming {
  public abstract class AimingPath : MonoBehaviour {
    public Vector3[] path { get; protected set; }

    public abstract void Calculate(Vector3 basePosition, Vector3 targetPosition);
  }
}