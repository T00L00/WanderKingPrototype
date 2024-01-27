using System.Collections;
using System.Collections.Generic;
using TooLoo.AI;
using UnityEngine;

namespace WK
{
    public class FollowerAI : MonoBehaviour
    {
        [SerializeField] private MoveController mover;

        public void Follow(Vector3 position)
        {
            mover.MoveTo(position);
        }
    }
}