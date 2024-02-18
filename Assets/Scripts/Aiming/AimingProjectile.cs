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
        
        private void Awake()
        {
            startPosition = transform.position;
        }
        
        public void Launch(AbstractTrajectoryPath trajectoryPath)
        {
            this.trajectoryPath = trajectoryPath;
            StartCoroutine(Coroutine_Movement());
        }

        private IEnumerator Coroutine_Movement()
        {
            float t = 0f;
            float time = trajectoryPath.duration;
            while (t < time)
            {
                transform.position = trajectoryPath.CalculatePositionAtTime(t);
                t += Time.deltaTime * speedMultiplier;
                yield return null;
            }
        }
    }
}
