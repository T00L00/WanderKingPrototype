using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WK
{
    public class ClickToMoveInputHandler : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private MoveController mover;
        [SerializeField] private LayerMask groundLayer;

        private Vector3 targetPosition;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Mouse.current.rightButton.isPressed)
            {
                Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, float.MaxValue, groundLayer))
                {
                    targetPosition = hit.point;
                }

                mover.MoveTo(targetPosition);
            }
        }
    }
}