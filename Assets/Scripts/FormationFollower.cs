using System.Collections;
using System.Collections.Generic;
using TooLoo.AI;
using UnityEngine;

namespace WK
{
    public class FormationFollower : MonoBehaviour
    {
        [SerializeField] private MoveController mover;

        /// <summary>
        /// The empty gameobject parent holding all this follower and other followers
        /// </summary>
        public Transform ContainerParent
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