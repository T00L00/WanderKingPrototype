using System;
using System.Collections.Generic;
using UnityEngine;
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
      private AbstractAiming currentAttack => availableAttacks[currentAttackIndex];
      
      [SerializeField] private GameObject projectilePrefab;
      [SerializeField] private List<AbstractAiming> availableAttacks = new List<AbstractAiming>();
      private int currentAttackIndex;
      
      private bool isAimModeEnabled;

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
        
        GameObject projectile = Instantiate(projectilePrefab, projectileStartPoint.position, Quaternion.identity);
        projectile.SetActive(true);
        AimingProjectile aimingProjectile = projectile.GetComponent<AimingProjectile>();
        aimingProjectile.Launch(currentAttack.TrajectoryPath);
      }
      
      public void SetAimPosition(Vector2 cursorPosition)
      {
        if (!isAimModeEnabled) return;
        
        Ray ray = Camera.main.ScreenPointToRay(cursorPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
        {
          aimReticle.transform.position =  new Vector3(hit.point.x, 0.01f, hit.point.z);
          currentAttack.DrawPath(projectileStartPoint.position, hit.point);
        }
      }

      public void NextUnit()
      {
        currentAttackIndex = (currentAttackIndex + 1) % availableAttacks.Count;
      }
    }
}
