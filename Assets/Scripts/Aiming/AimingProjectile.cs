using System;
using System.Collections;
using UnityEngine;

namespace WK.Aiming
{
    public class AimingProjectile : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier = 4f;
        private Vector3 startPosition;
        private AbstractProjectileTrajectory projectileTrajectory;
        
        private void Awake()
        {
            startPosition = transform.position;
        }
        
        public void Launch(AbstractProjectileTrajectory projectileTrajectory)
        {
            this.projectileTrajectory = projectileTrajectory;
            StartCoroutine(Coroutine_Movement());
        }

        private IEnumerator Coroutine_Movement()
        {
            float t = 0f;
            float time = projectileTrajectory.duration;
            while (t < time)
            {
                transform.position = projectileTrajectory.CalculatePositionFromTime(t);
                t += Time.deltaTime * speedMultiplier;
                yield return null;
            }
        }
    }
}
