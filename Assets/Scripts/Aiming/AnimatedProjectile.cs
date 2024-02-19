using UnityEngine;

namespace WK.Aiming
{
    public class AnimatedProjectile : AbstractProjectile
    {
        [SerializeField] private float speedMultiplier = 4f;
        private AbstractTrajectoryPath trajectoryPath;
        private float elapsedTime;
        
        public override void Launch(AbstractTrajectoryPath trajectoryPath)
        {
            this.trajectoryPath = trajectoryPath;
            
            Vector3 targetPosition = trajectoryPath.destination;
            Vector3 targetXZPos = new Vector3(targetPosition.x, 0.0f, targetPosition.z);
            transform.LookAt(targetXZPos);

            transform.position = trajectoryPath.startPosition;
            
            elapsedTime = 0f;
        }

        private void Update() {
            if (trajectoryPath == null) return;
            transform.position = trajectoryPath.CalculatePositionAtTime(elapsedTime);
            elapsedTime += (Time.deltaTime * speedMultiplier);
            
            if (elapsedTime > trajectoryPath.duration)
            {
                trajectoryPath = null;
            }
        }

    }
}
