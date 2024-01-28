using UnityEngine;

namespace WK {
  public class DirectionalMoveInputController : MonoBehaviour
  {
    
    [SerializeField] private float speed = 1f;
        
    public void Move(Vector3 moveDirection)
    {
      transform.position += moveDirection * Time.deltaTime * speed;
    }
  }
}