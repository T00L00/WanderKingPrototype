using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicTrajectory : MonoBehaviour
{
    [SerializeField] private Transform TargetObject;
    [Range(1.0f, 6.0f)] public float TargetRadius;
    [Range(20.0f, 75.0f)] public float LaunchAngle;

    private bool bTargetReady;

    private Rigidbody rigid;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Use this for initialization
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        bTargetReady = true;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // launches the object towards the TargetObject with a given LaunchAngle
    private void Launch()
    {
        bTargetReady = false;
    }

    // Sets a random target around the object based on the TargetRadius
    private void SetNewTarget() {
        bTargetReady = true;
    }

    // resets the projectile to its initial position
    private void ResetToInitialState()
    {
        rigid.velocity = Vector3.zero;
        transform.SetPositionAndRotation(initialPosition, initialRotation);
        bTargetReady = false;
    }
    
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bTargetReady)
            {
                Launch();
            }
            else
            {
                ResetToInitialState();
                SetNewTarget();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetToInitialState();
        }
    }
}
