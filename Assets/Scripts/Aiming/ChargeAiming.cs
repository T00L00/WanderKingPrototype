using UnityEngine;

namespace WK.Aiming
{
  public class ChargeAiming : AbstractAiming
  {
    
    [SerializeField] private AbstractTrajectoryRender abstractTrajectoryRender;

    private Vector3 startPosition;
    

    public override void Init() {
      projectileTrajectory = new ChargeTrajectory();
      gameObject.SetActive(true);
      abstractTrajectoryRender.Init();
    }

    public override void DrawPath(Vector3 startPosition, Vector3 endPosition)
    {
      Vector3[] path = projectileTrajectory.GetPath(startPosition, endPosition);
      abstractTrajectoryRender.SetPath(path);
    }

    public override void Clear()
    {
      gameObject.SetActive(false);
      abstractTrajectoryRender.Clear();
    }

  }
}