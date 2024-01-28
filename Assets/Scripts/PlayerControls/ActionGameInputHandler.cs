using UnityEngine;
using UnityEngine.InputSystem;

namespace WK
{
    public class ActionGameInputHandler : MonoBehaviour, PlayerControls.IActionGameControlsActions
    {
        [SerializeField] private Camera cam;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private DirectionalMoveInputController moveController;
        [SerializeField] private AimingController aimingController;
        
        private PlayerControls playerControls;
        private Vector2 cursorPosition;
        private Vector2 cursorDirection;
        private Vector3 moveDirection;
        private bool isMoving;
        
        void Awake()
        {
            playerControls = new PlayerControls();
            playerControls.ActionGameControls.SetCallbacks(this);
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }
        
        private void Update()
        {
            cursorPosition += cursorDirection * (Time.deltaTime * 10f);
            if (isMoving)
            {
                moveController.Move(moveDirection);
            }
            aimingController.SetAimPosition(cursorPosition);
        }
        
        public void OnMovementDirection(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            isMoving = direction.magnitude != 0;
            if (!isMoving) return;
            
            moveDirection = new Vector3(direction.x, 0f, direction.y);
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

        public void OnSecondaryAction(InputAction.CallbackContext context)
        {
            
        }

        public void OnNextUnit(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
        
        }

        public void OnAimPosition(InputAction.CallbackContext context)
        {
            cursorPosition = context.ReadValue<Vector2>();
            cursorDirection = Vector3.zero;
            cursorPosition += cursorDirection * Time.deltaTime * 10f;
        }

        public void OnAimMovement(InputAction.CallbackContext context)
        {
            cursorDirection = context.ReadValue<Vector3>();
        }

    }
}
