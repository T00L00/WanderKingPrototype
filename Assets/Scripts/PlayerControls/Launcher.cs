using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using WK.Aiming;

namespace WK
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private FormationLeader formationLeader;

        [Header("Aiming")]
        [SerializeField] private GameObject aimReticle;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private bool showTrajectory = true;

        private FormationFollower activeFollower;
        private Transform activeFollowerParent;

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
            // Return previous follower to its original leader
            if (activeFollower != null)
            {
                activeFollower.EnableNavmeshAgent(true);
                activeFollower.FormationParent = activeFollowerParent;
                formationLeader.ReturnFollower(activeFollower);
            }

            activeFollower = formationLeader.GetNextFollower();
            activeFollowerParent = activeFollower.FormationParent;

            activeFollower.EnableNavmeshAgent(false);
            activeFollower.FormationParent = transform.parent;
            StartCoroutine(MoveToPosition(transform.position, 0.5f));
        }

        IEnumerator MoveToPosition(Vector3 target, float duration)
        {
            Vector3 startPosition = activeFollower.UnitParent.position;
            float startTime = Time.time;
            float fraction = 0;

            while (fraction < 1)
            {
                fraction = (Time.time - startTime) / duration;
                activeFollower.UnitParent.position = Vector3.Lerp(startPosition, target, fraction);
                yield return null;
            }

            isChambered = true;
        }

        public void EnableAimMode()
        {
            if (!isChambered) return;

            isAimModeEnabled = true;
            aimReticle.SetActive(true);
            OnEnterAimMode?.Invoke();
        }
        public void DisableAimMode()
        {
            if (!isChambered) return;

            isAimModeEnabled = false;
            aimReticle.SetActive(false);
            activeFollower.ProjectileBehavior.Clear();
            OnExitAimMode?.Invoke();
        }

        public void LaunchProjectile()
        {
            if (!isAimModeEnabled) return;

            activeFollower.FormationParent = null;
            activeFollower.ProjectileBehavior.Launch();
        }
      
        private void SetAimPosition(Vector2 cursorPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(cursorPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
            {
                aimReticle.transform.position = new Vector3(hit.point.x, 0.01f, hit.point.z);
                activeFollower.ProjectileBehavior.CalculatePath(hit.point);
                //currentAttack.CalculatePath(activeFollower.transform.position, hit.point);
                if (showTrajectory) 
                {
                    //currentAttack.DrawPath();
                    activeFollower.ProjectileBehavior.DrawPath();
                }
            }
        }
    }
}
