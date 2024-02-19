using UnityEngine;

public class KinematicProjectile : MonoBehaviour
{
    [Range(20.0f, 75.0f)] public float launchAngle = 20f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrajectoryOrientation lookAtType = TrajectoryOrientation.FaceInitialDirection;
    
    private bool isInAir;
    private Rigidbody rigidBody;
    private Quaternion topInitialRotation = Quaternion.Euler(90, 0, 0);

    private enum TrajectoryOrientation {
        FaceInitialDirection,
        AlongPath,
        Top
    }
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    
    void Update ()
    {
        if (isInAir)
        {
            switch (lookAtType) {
                case TrajectoryOrientation.AlongPath:
                    transform.rotation = Quaternion.LookRotation(rigidBody.velocity);
                    break;
                case TrajectoryOrientation.Top:
                    transform.rotation = Quaternion.LookRotation(rigidBody.velocity) * topInitialRotation;
                    break;
                default:
                    break;
            }
        }
    } 

    public void LaunchProjectile(Vector3 targetPosition)
    {
        isInAir = true;
        
        Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 targetXZPos = new Vector3(targetPosition.x, 0.0f, targetPosition.z);
    
        transform.LookAt(targetXZPos);
        
        float distance = Vector3.Distance(projectileXZPos, targetXZPos);
        float gravity = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(launchAngle * Mathf.Deg2Rad);
        float height = targetPosition.y - transform.position.y;
        float Vz = Mathf.Sqrt(gravity * distance * distance / (2.0f * (height - distance * tanAlpha)) );
        float Vy = tanAlpha * Vz;
        
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        rigidBody.velocity = globalVelocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isInAir)
        {
            if (groundLayer == (groundLayer | (1 << other.gameObject.layer)))
            {
                HandleLanding(other.contacts[0].point);
            }
        }
    }

    private void HandleLanding(Vector3 contactPoint)
    {
        isInAir = false;
        transform.rotation = Quaternion.LookRotation(rigidBody.velocity, Vector3.up);
        rigidBody.velocity = Vector3.zero;
        transform.position = contactPoint;
    }
}
