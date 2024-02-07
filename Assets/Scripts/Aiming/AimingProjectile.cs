using System;
using System.Collections;
using UnityEngine;
using WK.Aiming;

public class AimingProjectile : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform projectileStartPoint;
    [SerializeField] private AimingPath aimingPath;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            aimingPath.Calculate( projectileStartPoint.position, hit.point);
            lineRenderer.positionCount = aimingPath.path.Length;
            for (var i = 0; i < aimingPath.path.Length; i++)
            {
                lineRenderer.SetPosition(i, aimingPath.path[i]);
            }

            // Debug.Log($"{v0} | {angle} | {time}");
            // DrawParabolicPath(groundDirection.normalized, v0, angle, time, _step);
            // if (Input.GetKeyDown(KeyCode.Space)) {
            //     StopAllCoroutines();
            //     StartCoroutine(Coroutine_Movement(groundDirection.normalized, _initialVelocity, angle, time));
            // }
        }
    }

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
