using UnityEngine;

namespace WK.Aiming {
  public abstract class AbstractTrajectoryRender : MonoBehaviour
  {
    public abstract void Init();
    public abstract void Clear();
    public abstract void SetPath(Vector3[] path);
  }
}