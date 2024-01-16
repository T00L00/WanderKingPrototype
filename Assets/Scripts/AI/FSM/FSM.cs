using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI
{
    public class FSM : IFSM
    {
        private IState currentState;
        private IState defaultState;

        public IState CurrentState
        {
            get { return currentState; }
            set
            {
                TransitionTo(value);
            }
        }

        public IState DefaultState
        {
            get { return defaultState; }
            set
            {
                if (currentState is null)
                {
                    currentState = value;
                }

                defaultState = value;
                defaultState.OnEnter();
            }
        }

        public FSM(IState defaultState=null)
        {
            this.currentState = defaultState;
            this.defaultState = defaultState;
        }

        public void TransitionToDefaultState()
        {
            if (currentState != null)
            {
                currentState.OnExit();
            }

            currentState = defaultState;
            currentState.OnEnter();
        }

        public void TransitionTo(IState state)
        {
            if (currentState != null && currentState != state)
            {
                currentState.OnExit();
            }

            currentState = state;
            currentState.OnEnter();
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.OnUpdate();
            }
        }

        public void Clear()
        {
            if (currentState != null)
            {
                currentState.OnExit();
                currentState = null;
            }

            defaultState = null;
        }
    }
}