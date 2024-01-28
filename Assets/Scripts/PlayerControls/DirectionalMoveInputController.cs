using UnityEngine;

namespace WK {
  public class DirectionalMoveInputController : MonoBehaviour
  {
    
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform directionTransform;
    [SerializeField] private Transform movementTransform;
    
    private Vector3 lastDirection = Vector3.forward;
    
    public void Move(Vector3 moveDirection)
    {
      movementTransform.position += moveDirection * Time.deltaTime * speed;
      lastDirection = moveDirection;
      UpdateDirection();
    }

    public void UpdateDirection()
    {
      if (directionTransform == null) return;
      if (lastDirection == Vector3.zero) return;
      
      Quaternion toRotation = Quaternion.LookRotation(lastDirection);
      directionTransform.rotation = toRotation;
    }
  }
}