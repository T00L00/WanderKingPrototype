using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI
{
    public interface IFSM
    {
        public void TransitionToDefaultState();

        public void TransitionTo(IState state);
    }
}

