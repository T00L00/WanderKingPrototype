using UnityEngine;

namespace WK.Aiming
{
  public class ParabolicAiming : AbstractAiming
  {
    
    [SerializeField] private float step;
    [SerializeField] private float maxHeight;
    [SerializeField] private float gravity;
    [SerializeField] private AbstractTrajectoryRender abstractTrajectoryRender;
    
    private Vector3 startPosition;
    
    public override void Init()
    {
      TrajectoryPath = new ParabolicTrajectoryPath(step, gravity, maxHeight);
      gameObject.SetActive(true);
      abstractTrajectoryRender.Init();
    }
    
    public override void DrawPath(Vector3 startPosition, Vector3 endPosition) {
      Vector3[] path = TrajectoryPath.GetPath(startPosition, endPosition);
      abstractTrajectoryRender.SetPath(path);
    }

    public override void Clear()
    {
      gameObject.SetActive(false);
      abstractTrajectoryRender.Clear();
    }

  }
}