using UnityEngine;
using UnityEngine.InputSystem;

namespace WK
{
    public class MobaGameInputHandler : MonoBehaviour, PlayerControls.IMobaGameControlsActions
    {
        [SerializeField] private Camera cam;
        [SerializeField] private MoveController moveController;
        [SerializeField] private AimingController aimingController;
        [SerializeField] private Chambering launcher;

        [SerializeField] private LayerMask groundLayer;

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
            aimingController?.SetAimPosition(cursorPosition);
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

        // Add Lctrl to toggle chambering?

        public void OnNextUnit(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            launcher.ChamberFollower();
            
            aimingController.DisableAimMode();
            aimingController.NextUnit();
        }

        public void OnAimMode(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                aimingController.EnableAimMode();
            } else if (context.canceled)
            {
                aimingController.LaunchProjectile();
                aimingController.DisableAimMode();
            }
        }

        public void OnAimPosition(InputAction.CallbackContext context)
        {
            cursorPosition = context.ReadValue<Vector2>();
        }
    }
}
