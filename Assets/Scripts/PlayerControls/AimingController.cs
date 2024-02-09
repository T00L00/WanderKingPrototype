using System;
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
      [SerializeField] private AbstractAiming abstractAiming;
      [SerializeField] private GameObject projectilePrefab;
      
      private bool isAimModeEnabled;

      public void EnableAimMode()
      {
        isAimModeEnabled = true;
        aimReticle.SetActive(true);
        abstractAiming.Init();
        OnEnterAimMode?.Invoke();
      }
      
      public void DisableAimMode()
      {
        isAimModeEnabled = false;
        aimReticle.SetActive(false);
        
        GameObject projectile = Instantiate(projectilePrefab, projectileStartPoint.position, Quaternion.identity);
        projectile.SetActive(true);
        AimingProjectile aimingProjectile = projectile.GetComponent<AimingProjectile>();
        aimingProjectile.LaunchProjectile(abstractAiming);
        
        abstractAiming.Clear();
        OnExitAimMode?.Invoke();
      }
      
      public void SetAimPosition(Vector2 cursorPosition)
      {
        if (!isAimModeEnabled) return;
        
        Ray ray = Camera.main.ScreenPointToRay(cursorPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
        {
          aimReticle.transform.position =  new Vector3(hit.point.x, 0.01f, hit.point.z);
          abstractAiming.DrawPath(projectileStartPoint.position, hit.point);
        }
      }
    }
}
