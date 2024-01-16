using System.Collections;
using System.Collections.Generic;
using TooLoo.AI;
using UnityEngine;

namespace WK
{
    public class FollowerAI : MonoBehaviour
    {
        [SerializeField] private MoveController mover;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Follow(Vector3 position)
        {
            mover.MoveTo(position);
        }
    }
}