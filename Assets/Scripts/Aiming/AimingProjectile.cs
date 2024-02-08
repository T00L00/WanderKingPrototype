using System;
using System.Collections;
using UnityEngine;
using WK.Aiming;

public class AimingProjectile : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 4f;
    private Vector3 startPosition;
    private AimingPath aimingPath;
    
    private void Awake()
    {
        startPosition = transform.position;
    }
    
    public void LaunchProjectile(AimingPath aimingPath) {
        this.aimingPath = aimingPath;
        float time = aimingPath.Duration;
        StartCoroutine(Coroutine_Movement(time));
    }

    private IEnumerator Coroutine_Movement(float time)
    {
        float t = 0f;
        
        while (t < time)
        {
            transform.position = aimingPath.CalculatePositionFromTime(t);
            t += Time.deltaTime * speedMultiplier;
            yield return null;
        }
    }
}
