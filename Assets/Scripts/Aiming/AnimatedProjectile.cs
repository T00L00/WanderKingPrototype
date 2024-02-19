using System;
using System.Collections;
using UnityEngine;

namespace WK.Aiming
{
    public class AnimatedProjectile : AbstractProjectile
    {
        [SerializeField] private float speedMultiplier = 4f;
        private Vector3 startPosition;
        private AbstractTrajectoryPath trajectoryPath;
        private float elapsedTime;
        
        public override void Launch(AbstractTrajectoryPath trajectoryPath)
        {
            this.trajectoryPath = trajectoryPath;
            elapsedTime = 0f;
        }
        
        private void Awake()
        {
            startPosition = transform.position;
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
