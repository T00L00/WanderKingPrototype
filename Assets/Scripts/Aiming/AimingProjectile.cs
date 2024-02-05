using System;
using System.Collections;
using UnityEngine;
using WK.Aiming;

public class AimingProjectile : MonoBehaviour {
    [SerializeField] private float _initialVelocity = 10f;
    [SerializeField] private float _angle = 45f;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private float _step = 0.1f;
    [SerializeField] private float _height = 0.1f;
    [SerializeField] private Transform _firePoint;
    private AimingPath aimingPath;
    
    private void Awake() {
        aimingPath = new ParabolicAimingPath(_step, 9.8f);
        
    }

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            // Vector3 direction = hit.point - _firePoint.position;
            // Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
            // Vector3 targetPosition = new Vector3(groundDirection.magnitude, direction.y, 0f);
            
            // Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _firePoint.position;
            // float height = targetPosition.y + targetPosition.magnitude / 2f;
            // height = Mathf.Max(0.1f, height);
            aimingPath.Calculate( _firePoint.position, hit.point);
            _line.positionCount = aimingPath.path.Length;
            for (var i = 0; i < aimingPath.path.Length; i++) {
                _line.SetPosition(i, aimingPath.path[i]);
            }

            // Debug.Log($"{v0} | {angle} | {time}");
            // DrawParabolicPath(groundDirection.normalized, v0, angle, time, _step);
            // if (Input.GetKeyDown(KeyCode.Space)) {
            //     StopAllCoroutines();
            //     StartCoroutine(Coroutine_Movement(groundDirection.normalized, _initialVelocity, angle, time));
            // }
        }
    }

    private void DrawParabolicPath(Vector3 direction, float v0, float angle, float time, float step) {
        _step = Mathf.Max(0.01f, step);
        
        float gravity = 9.8f;
        _line.positionCount = (int)(time / _step) + 2;

        int count = 0;
        for (float t = 0; t < time; t += _step) {
            float x = v0 * Mathf.Cos(angle) * t;
            float y = v0 * Mathf.Sin(angle) * t - 0.5f * gravity * Mathf.Pow(t, 2);
            _line.SetPosition(count, _firePoint.position + direction*x + Vector3.up*y);
            count++;
        }

        float xFinal = v0 * time * Mathf.Cos(angle);
        float yFinal = v0 * time * Mathf.Sin(angle) - 0.5f * gravity * Mathf.Pow(time, 2);
        _line.SetPosition(count, _firePoint.position + direction*xFinal + Vector3.up*yFinal);
    }
    
    private float QuadraticEquation(float a, float b, float c, bool positive) {
        float sign = positive ? 1 : -1;
        float determinant = Mathf.Pow(b, 2) - 4 * a * c;
        if (determinant < 0) return float.NaN;
        float sqrt = sign * Mathf.Sqrt(determinant);
        float result = (-b + sqrt) / (2 * a);
        return result;
    }

    private void CalculatePathWithHeight(Vector3 target, float height, out float v0, out float angle, out float time) {
        float targetX = target.x;
        float targetY = target.y;
        float gravity = 9.8f;
        
        float b = Mathf.Sqrt(2 * gravity * height);
        float a = (-0.5f * gravity);
        float c = -targetY;

        float tplus = QuadraticEquation(a, b, c, true);
        float tminus = QuadraticEquation(a, b, c, false);
        time = tplus > tminus ? tplus : tminus;
        
        angle = Mathf.Atan(b * time / targetX);
        v0 = b / Mathf.Sin(angle);
    }
    //
    // private void CalculatePath(Vector3 target, float angle, out float v0, out float time) {
    //     float xt = target.x;
    //     float yt = target.y;
    //     float gravity = -9.8f;
    //
    //     float v1 = Mathf.Pow(xt, 2) * gravity;
    //     float v2 = 2 * xt * Mathf.Sin(angle)  * Mathf.Cos(angle);
    //     float v3 = 2 * yt * Mathf.Pow(Mathf.Cos(angle), 2);
    //     v0 = Mathf.Sqrt(v1 / (v2 - v3));
    //
    //     time = xt / (v0 * Mathf.Cos(angle));
    // }
    //
    private IEnumerator Coroutine_Movement(Vector3 direction, float v0, float angle, float time) {
        float t = 0f;
        float gravity = -9.8f;
        // float gravity = Physics.gravity.y;
        while (t < time) {
            float x = v0 * Mathf.Cos(angle) * t;
            float y = v0 * Mathf.Sin(angle) * t - 0.5f * gravity * Mathf.Pow(t, 2);
            transform.position = _firePoint.position + direction*x + Vector3.up*y;
            t += Time.deltaTime;
            yield return null;
        }
    }
}
