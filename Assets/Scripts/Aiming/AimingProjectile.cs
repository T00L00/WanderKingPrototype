using System;
using System.Collections;
using UnityEngine;

namespace WK.Aiming
{
    public class AimingProjectile : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier = 4f;
        private Vector3 startPosition;
        private AbstractAiming abstractAiming;
        
        private void Awake()
        {
            startPosition = transform.position;
        }
        
        public void Launch(AbstractAiming abstractAiming) {
            this.abstractAiming = abstractAiming;
            float time = abstractAiming.Duration;
            StartCoroutine(Coroutine_Movement(time));
        }

        private IEnumerator Coroutine_Movement(float time)
        {
            float t = 0f;
            
            while (t < time)
            {
                transform.position = abstractAiming.CalculatePositionFromTime(t);
                t += Time.deltaTime * speedMultiplier;
                yield return null;
            }
        }
    }
}
