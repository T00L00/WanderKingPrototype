using UnityEngine;

namespace WK.Aiming
{
  public class ParabolicAiming : AbstractAiming
  {
    
    [SerializeField] private float step;
    [SerializeField] private float maxHeight;
    [SerializeField] private float gravity;
    [SerializeField] private ProjectileTrajectoryRenderer trajectoryRenderer;
    
    private Vector3 startPosition;
    
    public override void Init()
    {
      TrajectoryPath = new ParabolicProjectileTrajectory(step, gravity, maxHeight);
      gameObject.SetActive(true);
      trajectoryRenderer.Init();
    }
    
    public override void CalculatePath(Vector3 startPosition, Vector3 endPosition) {
      TrajectoryPath.CalculatePath(startPosition, endPosition);
    }
    
    public override void DrawPath() {
      trajectoryRenderer.SetPath(TrajectoryPath.GetPath());
    }

    public override void Clear()
    {
      gameObject.SetActive(false);
      trajectoryRenderer.Clear();
    }

  }
}