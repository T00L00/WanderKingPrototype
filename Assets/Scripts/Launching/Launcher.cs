using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using WK.Aiming;

namespace WK
{
    /// <summary>
    /// Create a child object with this component in order to launch followers that have ProjectileBehavior component
    /// </summary>
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private FormationManager formationManager;

        [Header("Aiming")]
        [SerializeField] private GameObject aimReticle;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private bool showTrajectory = true;

        private FormationFollower activeCaptain;

        private Coroutine chargingCoroutine;

        private bool isAimModeEnabled = false;
        private bool isChambered = false;

        public Vector3 CursorPosition { get; set; }
        public Vector3 TargetPosition => aimReticle.transform.position;

        public Action OnEnterAimMode;
        public Action OnExitAimMode;

        private void Update()
        {
            if (!isChambered) return;
            if (!isAimModeEnabled) return;

            // Set aim position
            SetAimPosition(CursorPosition);
        }

        public void ChamberFollower()
        {
            activeCaptain = formationManager.GetCaptainForChambering();
            StartCoroutine(MoveToPosition(0.5f));
        }

        IEnumerator MoveToPosition(float duration)
        {
            Vector3 startPosition = activeCaptain.UnitParent.position;
            float startTime = Time.time;
            float fraction = 0;

            while (fraction < 1)
            {
                fraction = (Time.time - startTime) / duration;
                activeCaptain.UnitParent.position = Vector3.Lerp(startPosition, transform.position, fraction);
                yield return null;
            }

            isChambered = true;
        }

        IEnumerator ChargeActiveCaptain()
        {
            float chargeTime = 0;
            while (isChambered)
            {
                chargeTime += Time.deltaTime;

                if (chargeTime >= 1)
                {
                    chargeTime = 0;
                    formationManager.ChargeChamberedCaptain();
                }

                yield return null;
            }
        }

        public void EnableAimMode()
        {
            if (!isChambered) return;

            isAimModeEnabled = true;
            aimReticle.SetActive(true);

            // Start charging
            chargingCoroutine = StartCoroutine(ChargeActiveCaptain());

            OnEnterAimMode?.Invoke();
        }

        // TODO - If you quickly press and release space before unit is fully in chambered position, unit gets stuck in chambered position
        public void DisableAimMode()
        {
            if (!isChambered) return;

            isChambered = false;
            isAimModeEnabled = false;
            aimReticle.SetActive(false);
            activeCaptain.ProjectileBehavior.Clear();
            activeCaptain = null;
            OnExitAimMode?.Invoke();
        }

        public void LaunchFollower()
        {
            if (!isChambered) return;
            if (!isAimModeEnabled) return;

            // Stop charging
            StopCoroutine(chargingCoroutine);

            activeCaptain.DetachFromFormationParent();
            activeCaptain.ProjectileBehavior.Launch();
        }
      
        private void SetAimPosition(Vector2 cursorPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(cursorPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
            {
                aimReticle.transform.position = new Vector3(hit.point.x, 0.01f, hit.point.z);
                activeCaptain.ProjectileBehavior.CalculatePath(transform.position, hit.point);
                if (showTrajectory) 
                {
                    activeCaptain.ProjectileBehavior.DrawPath();
                }
            }
        }
    }
}
