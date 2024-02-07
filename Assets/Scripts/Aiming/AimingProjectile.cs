using System;
using System.Collections;
using UnityEngine;
using WK.Aiming;

public class AimingProjectile : MonoBehaviour
{
    [SerializeField] private Transform projectileStartPoint;


    private IEnumerator Coroutine_Movement(Vector3 direction, float v0, float angle, float time)
    {
        float t = 0f;
        float gravity = -9.8f;
        // float gravity = Physics.gravity.y;
        while (t < time)
        {
            float x = v0 * Mathf.Cos(angle) * t;
            float y = v0 * Mathf.Sin(angle) * t - 0.5f * gravity * Mathf.Pow(t, 2);
            transform.position = projectileStartPoint.position + direction*x + Vector3.up*y;
            t += Time.deltaTime;
            yield return null;
        }
    }
}
