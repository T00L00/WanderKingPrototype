using System;
using UnityEngine;

namespace WK {
  public abstract class GameplayInputHandler : MonoBehaviour
  {
    public static EventHandler<EventNextUnitArgs> HandleNextUnit;
    public class EventNextUnitArgs : EventArgs { }

    public static EventHandler<EventAimModeArgs> HandleEnterAimMode;
    public static EventHandler<EventAimModeArgs> HandleExitAimMode;
    public class EventAimModeArgs : EventArgs {
            
    }
  }
}