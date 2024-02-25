using UnityEngine;

namespace WK.Aiming
{
  public class RunForwardAiming : AbstractAiming
  {
    [SerializeField] private ProjectileTrajectoryRenderer abstractTrajectoryRender;

    private Vector3 startPosition;

    public override void CalculatePath(Vector3 startPosition, Vector3 endPosition) {
      TrajectoryPath.CalculatePath(startPosition, endPosition);
    }

    public override void Init() {
      TrajectoryPath = new RunForwardTrajectoryPath();
      gameObject.SetActive(true);
      abstractTrajectoryRender.Init();
    }

    public override void DrawPath()
    {
      abstractTrajectoryRender.SetPath(TrajectoryPath.GetPath());
    }

    public override void Clear()
    {
      gameObject.SetActive(false);
      abstractTrajectoryRender.Clear();
    }

  }
}