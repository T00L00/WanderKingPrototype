using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace WK
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navigator;

        [Header("Settings")]
        [SerializeField, Tooltip("Automatically handle rotation")] 
        private bool handleRotation = false;

        [SerializeField] private float rotationSpeed = 10f;

        public Vector3 NavigatorPosition => navigator.transform.position;
        public Vector3 Velocity => navigator.velocity;
        public bool HasPath => navigator.hasPath;

        private void OnValidate()
        {
            if (handleRotation)
            {
                navigator.updateRotation = false;
            }
            else
            {
                navigator.updateRotation = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            HandleRotation();
        }

        public void Init()
        {
            
        }

        public void MoveTo(Vector3 position)
        {
            navigator.SetDestination(position);
        }

        /// <summary>
        /// Slerps rotation to face navmesh path direction
        /// </summary>
        public void HandleRotation()
        {
            if (!handleRotation) return;

            if (navigator.hasPath)
            {
                Vector3 facing = navigator.steeringTarget - navigator.transform.position;
                facing.y = 0;
                facing.Normalize();

                Quaternion targetRotation = Quaternion.LookRotation(facing, Vector3.up);
                Quaternion nextRotation = Quaternion.Slerp(navigator.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                navigator.transform.rotation = nextRotation;
            }
        }

        /// <summary>
        /// Slerp rotation towards given transform target
        /// </summary>
        /// <param name="target"></param>
        public void HandleRotation(Transform target)
        {
            if (navigator.hasPath)
            {
                Vector3 facing = target.position - navigator.transform.position;
                facing.y = 0;
                facing.Normalize();

                Quaternion targetRotation = Quaternion.LookRotation(facing, Vector3.up);
                Quaternion nextRotation = Quaternion.Slerp(navigator.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                navigator.transform.rotation = nextRotation;
            }
        }

        /// <summary>
        /// Immediately rotate towards given transform target
        /// </summary>
        /// <param name="target"></param>
        public void FaceTowards(Transform target)
        {
            Vector3 facing;
            facing = target.position - navigator.transform.position;
            facing.y = 0f;
            facing.Normalize();

            //Apply Rotation
            Quaternion targ_rot = Quaternion.LookRotation(facing, Vector3.up);
            Quaternion nrot = Quaternion.RotateTowards(navigator.transform.rotation, targ_rot, 360f);
            navigator.transform.rotation = nrot;
        }

        public bool HasReachedDestination()
        {
            return Vector3.Distance(navigator.transform.position, navigator.destination) <= 1f;
        }

        public float DistanceFrom(Vector3 position)
        {
            return Vector3.Distance(navigator.transform.position, position);
        }

        public Vector3 DirectionFrom(Vector3 position)
        {
            return (position - navigator.transform.position).normalized;
        }

        public void ResetPath()
        {
            navigator.ResetPath();
        }

        public void StopMovement()
        {
            navigator.speed = 0;
            navigator.velocity = Vector3.zero;
        }

        public void EnableMovement(bool state)
        {
            navigator.enabled = state;
        }

        public void SetSpeed(float speed)
        {
            navigator.speed = speed;
        }
    }
}