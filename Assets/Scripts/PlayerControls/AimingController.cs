using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WK.Aiming;

namespace WK
{
    public class AimingController : MonoBehaviour
    {
      
      public Action OnEnterAimMode;
      public Action OnExitAimMode;
   
      [SerializeField] private GameObject aimReticle;
      [SerializeField] private LayerMask layerMask;
      [SerializeField] private Transform projectileStartPoint;
      [SerializeField] private bool showTrajectory = true;
      
      private AbstractAiming currentAttack => availableAttacks[currentAttackIndex];
      
      [FormerlySerializedAs("projectilePrefab")] [SerializeField] private AbstractProjectile abstractProjectilePrefab;
      [SerializeField] private List<AbstractAiming> availableAttacks = new List<AbstractAiming>();
      private int currentAttackIndex;
      
      private bool isAimModeEnabled;
      public Vector3 targetPosition => aimReticle.transform.position;
      
      public void EnableAimMode()
      {
        isAimModeEnabled = true;
        aimReticle.SetActive(true);
        currentAttack.Init();
        OnEnterAimMode?.Invoke();
      }
      
      public void DisableAimMode()
      {
        isAimModeEnabled = false;
        aimReticle.SetActive(false);
        currentAttack.Clear();
        OnExitAimMode?.Invoke();
      }

      public void LaunchProjectile()
      {
        if (!isAimModeEnabled) return;
        
        AbstractProjectile abstractProjectile = Instantiate(abstractProjectilePrefab, projectileStartPoint.position, Quaternion.identity);
        abstractProjectile.gameObject.SetActive(true);
        abstractProjectile.transform.position = currentAttack.TrajectoryPath.startPosition;
        abstractProjectile.Launch(currentAttack.TrajectoryPath);
      }
      
      public void SetAimPosition(Vector2 cursorPosition)
      {
        if (!isAimModeEnabled) return;
        
        Ray ray = Camera.main.ScreenPointToRay(cursorPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
        {
          aimReticle.transform.position =  new Vector3(hit.point.x, 0.01f, hit.point.z);
          currentAttack.CalculatePath(projectileStartPoint.position, hit.point);
          if (showTrajectory) {
            currentAttack.DrawPath();
          }
        }
      }

      public void NextUnit()
      {
        currentAttackIndex = (currentAttackIndex + 1) % availableAttacks.Count;
      }
    }
}
