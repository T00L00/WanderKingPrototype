using System.Collections;
using System.Collections.Generic;
using TooLoo.AI;
using UnityEngine;
using WK.Aiming;

namespace WK
{
    public class FormationFollower : MonoBehaviour
    {
        [SerializeField] private MoveController mover;
        [SerializeField] private ProjectileBehavior projectileBehavior;

        /// <summary>
        /// The empty gameobject parent holding this follower and all other followers
        /// </summary>
        public Transform FormationParent
        {
            get => transform.parent.parent;
            set
            {
                transform.parent.parent = value;
            }
        }

        /// <summary>
        /// The actual unit gameobject parent
        /// </summary>
        public Transform UnitParent => transform.parent;

        public ProjectileBehavior ProjectileBehavior => projectileBehavior;

        public void Follow(Vector3 position)
        {
            mover.MoveTo(position);
        }

        public void EnableNavmeshAgent(bool state)
        {
            mover.EnableMovement(state);
        }
    }
}