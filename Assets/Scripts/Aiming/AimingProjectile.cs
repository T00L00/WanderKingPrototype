using System;
using System.Collections;
using UnityEngine;
using WK.Aiming;

public class AimingProjectile : MonoBehaviour
{
    private Vector3 startPosition;
    private float gravity = 9.8f;
    [SerializeField] private float speedMultiplier = 4f;
    
    private void Awake()
    {
        startPosition = transform.position;
    }
    
    public void LaunchProjectile(AimingPath aimingPath)
    {
        Vector3 direction = aimingPath.Direction;
        float v0 = aimingPath.SpeedStart;
        float angle = aimingPath.Angle;
        float time = aimingPath.Duration;
        StartCoroutine(Coroutine_Movement(direction, v0, angle, time));
    }

    private IEnumerator Coroutine_Movement(Vector3 direction, float v0, float angle, float time)
    {
        float t = 0f;
        
        while (t < time)
        {
            float x = v0 * Mathf.Cos(angle) * t;
            float y = v0 * Mathf.Sin(angle) * t - 0.5f * gravity * Mathf.Pow(t, 2);
            transform.position = startPosition + direction*x + Vector3.up*y;
            t += Time.deltaTime * speedMultiplier;
            yield return null;
        }
    }
}
