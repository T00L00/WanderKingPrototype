using System;
using UnityEngine;

namespace WK {
    public class AimingController : MonoBehaviour
    {
      
      public static Action OnEnterAimMode;
      public static Action OnExitAimMode;
   
      [SerializeField] private GameObject aimReticle;
      [SerializeField] private LayerMask layerMask;
      
      private bool isAimModeEnabled;

      public void EnableAimMode()
      {
        isAimModeEnabled = true;
        aimReticle.SetActive(true);
        OnEnterAimMode?.Invoke();
      }
      
      public void DisableAimMode()
      {
        isAimModeEnabled = false;
        aimReticle.SetActive(false);
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
        }
      }
    }
}
