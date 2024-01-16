using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI
{
    public abstract class State : MonoBehaviour, IState
    {
        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public abstract void OnUpdate();
    }
}