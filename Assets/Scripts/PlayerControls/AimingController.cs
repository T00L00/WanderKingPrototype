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
      [SerializeField] private AimingPath aimingPath;
      
      private bool isAimModeEnabled;

      public void EnableAimMode()
      {
        isAimModeEnabled = true;
        aimReticle.SetActive(true);
        aimingPath.Init();
        OnEnterAimMode?.Invoke();
      }
      
      public void DisableAimMode()
      {
        isAimModeEnabled = false;
        aimReticle.SetActive(false);
        aimingPath.Clear();
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
          aimingPath.DrawPath(projectileStartPoint.position, hit.point);
        }
      }
    }
}
