using UnityEngine;

namespace WK.Aiming
{
    /// <summary>
    /// Create child gameobject with this component for any unit that needs to be launched
    /// </summary>
    public class ProjectileBehavior : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier = 4f;

        [SerializeField] private float step;
        [SerializeField] private float maxHeight;
        [SerializeField] private float gravity;
        [SerializeField] private ProjectileTrajectoryRenderer trajectoryRenderer;

        private ProjectileTrajectory trajectory; // should not be stored here. Get from unit characteristics.

        private bool isInFlight;
        private float elapsedTime;

        private void Start()
        {
            isInFlight = false;
            elapsedTime = 0f;
            //trajectory = new ParabolicProjectileTrajectory(step, gravity, maxHeight);
            trajectory = new RunForwardProjectileTrajectory();
        }

        private void FixedUpdate() 
        {
            if (!isInFlight) return;

            transform.parent.position = trajectory.CalculatePositionAtTime(elapsedTime);
            elapsedTime += Time.fixedDeltaTime * speedMultiplier;
            
            if (elapsedTime > trajectory.Duration)
            {
                isInFlight = false;
                elapsedTime = 0;
            }
        }

        // TODO - Turn this into a coroutine
        public void Launch()
        {
            Vector3 targetPosition = trajectory.Destination;
            Vector3 targetXZPos = new Vector3(targetPosition.x, 0.0f, targetPosition.z);
            transform.parent.LookAt(targetXZPos);
            elapsedTime = 0f;
            isInFlight = true;
        }

        public void DrawPath()
        {
            trajectoryRenderer.SetPath(trajectory.GetPath());
        }

        public void CalculatePath(Vector3 startPosition, Vector3 endPosition)
        {
            trajectory.CalculatePath(startPosition, endPosition);
        }

        public void Clear()
        {
            trajectoryRenderer.Clear();
        }
    }
}
