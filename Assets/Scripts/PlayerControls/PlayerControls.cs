//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/PlayerControls/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace WK
{
    public partial class @PlayerControls: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PC"",
            ""id"": ""89b5dcb4-e733-4c78-93d7-314592468b03"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""14afb303-bd2c-4136-8cfe-f9b5a75778f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9449d38d-03b1-4d6f-ba85-ebbdc7e2cac1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // PC
            m_PC = asset.FindActionMap("PC", throwIfNotFound: true);
            m_PC_Move = m_PC.FindAction("Move", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // PC
        private readonly InputActionMap m_PC;
        private List<IPCActions> m_PCActionsCallbackInterfaces = new List<IPCActions>();
        private readonly InputAction m_PC_Move;
        public struct PCActions
        {
            private @PlayerControls m_Wrapper;
            public PCActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_PC_Move;
            public InputActionMap Get() { return m_Wrapper.m_PC; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PCActions set) { return set.Get(); }
            public void AddCallbacks(IPCActions instance)
            {
                if (instance == null || m_Wrapper.m_PCActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_PCActionsCallbackInterfaces.Add(instance);
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }

            private void UnregisterCallbacks(IPCActions instance)
            {
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
            }

            public void RemoveCallbacks(IPCActions instance)
            {
                if (m_Wrapper.m_PCActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IPCActions instance)
            {
                foreach (var item in m_Wrapper.m_PCActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_PCActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public PCActions @PC => new PCActions(this);
        public interface IPCActions
        {
            void OnMove(InputAction.CallbackContext context);
        }
    }
}
