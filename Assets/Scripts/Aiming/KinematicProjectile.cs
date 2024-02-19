using UnityEngine;

namespace WK.Aiming
{
    public class KinematicProjectile : AbstractProjectile
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private TrajectoryOrientation lookAtType = TrajectoryOrientation.FaceInitialDirection;

        private bool isInAir;
        private Rigidbody rigidBody;
        private Quaternion topInitialRotation = Quaternion.Euler(90, 0, 0);

        private enum TrajectoryOrientation
        {
            FaceInitialDirection,
            AlongPath,
            Top
        }

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (isInAir)
            {
                switch (lookAtType)
                {
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

        public override void Launch(AbstractTrajectoryPath trajectoryPath)
        {
            Vector3 targetPosition = trajectoryPath.destination;
            float launchAngle = Mathf.Max(20f * Mathf.Deg2Rad, trajectoryPath.launchAngle);
            isInAir = true;

            Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
            Vector3 targetXZPos = new Vector3(targetPosition.x, 0.0f, targetPosition.z);

            transform.LookAt(targetXZPos);

            float distance = Vector3.Distance(projectileXZPos, targetXZPos);
            float gravity = Physics.gravity.y;
            float tanAlpha = Mathf.Tan(launchAngle);
            float height = targetPosition.y - transform.position.y;
            float result = height - distance * tanAlpha;
            float Vz = Mathf.Sqrt(gravity * distance * distance / (2.0f * result));
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
}
