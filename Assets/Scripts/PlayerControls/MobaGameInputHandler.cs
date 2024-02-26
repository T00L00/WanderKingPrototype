using UnityEngine;
using UnityEngine.InputSystem;

namespace WK
{
    public class MobaGameInputHandler : MonoBehaviour, PlayerControls.IMobaGameControlsActions
    {
        [SerializeField] private Camera cam;
        [SerializeField] private MoveController moveController;
        [SerializeField] private Launcher launcher;

        [SerializeField] private LayerMask groundLayer;

        private PlayerControls playerControls;
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

        public void OnMoveCommand(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, groundLayer))
            {
                targetPosition = hit.point;
            }

            moveController.MoveTo(targetPosition);
        }

        // Add Lctrl to toggle chambering?

        public void OnChamberFollower(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            launcher.ChamberFollower();
        }

        public void OnAimMode(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                launcher.EnableAimMode();
            }
            else if (context.canceled)
            {
                launcher.LaunchFollower();
                launcher.DisableAimMode();
            }
        }

        public void OnAimPosition(InputAction.CallbackContext context)
        {
            launcher.CursorPosition = context.ReadValue<Vector2>();
        }
    }
}
