using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WK
{
    public class MobaGameInputHandler : GameplayInputHandler, PlayerControls.IMobaGameControlsActions
    {
        [SerializeField] private Camera cam;
        [SerializeField] private MoveController moveController;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private AimingController aimingController;

        private PlayerControls playerControls;
        private Vector2 cursorPosition;
        private Vector3 targetPosition;

        void Awake()
        {
            playerControls = new PlayerControls();
            playerControls.MobaGameControls.SetCallbacks(this);
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update() {
            aimingController.SetAimPosition(cursorPosition);
        }

        public void OnMoveCommand(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            
            Ray ray = cam.ScreenPointToRay(cursorPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, groundLayer))
            {
                targetPosition = hit.point;
            }

            moveController.MoveTo(targetPosition);
        }

        public void OnNextUnit(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            
            HandleNextUnit?.Invoke(this, new EventNextUnitArgs());
        }

        public void OnAimMode(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                aimingController.EnableAimMode();
            } else if (context.canceled)
            {
                aimingController.DisableAimMode();
            }
        }

        public void OnAimPosition(InputAction.CallbackContext context)
        {
            cursorPosition = context.ReadValue<Vector2>();
        }
    }
}
