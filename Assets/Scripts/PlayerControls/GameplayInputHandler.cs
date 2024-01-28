using System;
using UnityEngine;

namespace WK {
  public abstract class GameplayInputHandler : MonoBehaviour
  {
    public static EventHandler<EventNextUnitArgs> HandleNextUnit;
    public class EventNextUnitArgs : EventArgs { }

    
  }
}