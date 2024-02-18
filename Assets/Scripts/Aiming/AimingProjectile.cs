using System;
using System.Collections;
using UnityEngine;

namespace WK.Aiming
{
    public class AimingProjectile : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier = 4f;
        private Vector3 startPosition;
        private AbstractTrajectoryPath trajectoryPath;
        private float elapsedTime;
        
        private void Awake()
        {
            startPosition = transform.position;
        }
        
        public void Launch(AbstractTrajectoryPath trajectoryPath)
        {
            this.trajectoryPath = trajectoryPath;
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
