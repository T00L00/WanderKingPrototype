using UnityEngine;

namespace WK.Aiming {
  public abstract class AbstractProjectile : MonoBehaviour {
    public abstract void Launch(AbstractTrajectoryPath trajectoryPath);
  }
}